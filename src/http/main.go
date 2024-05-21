package main

import (
	"context"
	"encoding/json"
	"errors"
	"flag"
	"io"
	"log"
	"net"
	"net/http"
	"os"

	"bicep_local_providers/http/bicep.azure.com/protos/extension"

	"google.golang.org/grpc"
)

var (
	socket = flag.String("socket", "", "The path to the domain socket to connect on")
)

func main() {
	flag.Parse()

	if *socket == "" {
		log.Fatalf("missing required -%s argument\n", "--socket")
	}

	if _, err := os.Stat(*socket); !os.IsNotExist(err) {
		if err := os.RemoveAll(*socket); err != nil {
			log.Fatal(err)
		}
	}

	listener, err := net.Listen("unix", *socket)
	if err != nil {
		log.Fatalf("failed to listen: %v", err)
	}

	grpcServer := grpc.NewServer()
	extension.RegisterBicepExtensionServer(grpcServer, newServer())
	grpcServer.Serve(listener)
}

func newServer() *bicepExtensionServer {
	s := &bicepExtensionServer{}
	return s
}

type bicepExtensionServer struct {
	extension.UnimplementedBicepExtensionServer
}

func buildError(err error) *extension.ExtensibilityOperationResponse {
	return &extension.ExtensibilityOperationResponse{
		Errors: []*extension.ExtensibilityError{
			{
				Code:    "UnexpectedError",
				Message: err.Error(),
			},
		},
	}
}

func (s *bicepExtensionServer) Save(ctx context.Context, req *extension.ExtensibilityOperationRequest) (*extension.ExtensibilityOperationResponse, error) {
	if req.Resource.Type != "HttpRequest" {
		err := errors.New("invalid resource type")
		return buildError(err), nil
	}

	var httpRequest HttpRequest
	err := json.Unmarshal([]byte(*req.Resource.Properties), &httpRequest)

	if err != nil {
		return buildError(err), nil
	}

	err = validateRequest(&httpRequest)
	if err != nil {
		return buildError(err), nil
	}

	response, err := makeRequest(&httpRequest)
	if err != nil {
		return buildError(err), nil
	}

	properties, err := json.Marshal(*response)
	if err != nil {
		return buildError(err), nil
	}
	propertiesString := string(properties)

	return &extension.ExtensibilityOperationResponse{
		Resource: &extension.ExtensibleResourceData{
			Type:       req.Resource.Type,
			Properties: &propertiesString,
		},
	}, nil
}

func (s *bicepExtensionServer) PreviewSave(ctx context.Context, req *extension.ExtensibilityOperationRequest) (*extension.ExtensibilityOperationResponse, error) {
	if req.Resource.Type != "HttpRequest" {
		err := errors.New("invalid resource type")
		return buildError(err), nil
	}

	var httpRequest HttpRequest
	err := json.Unmarshal([]byte(*req.Resource.Properties), &httpRequest)

	if err != nil {
		return buildError(err), nil
	}

	err = validateRequest(&httpRequest)
	if err != nil {
		return buildError(err), nil
	}

	return &extension.ExtensibilityOperationResponse{
		Resource: req.Resource,
	}, nil
}

func (s *bicepExtensionServer) Get(ctx context.Context, req *extension.ExtensibilityOperationRequest) (*extension.ExtensibilityOperationResponse, error) {
	if req.Resource.Type != "HttpRequest" {
		return nil, errors.New("invalid resource type")
	}

	return nil, errors.ErrUnsupported
}

func (s *bicepExtensionServer) Delete(ctx context.Context, req *extension.ExtensibilityOperationRequest) (*extension.ExtensibilityOperationResponse, error) {
	if req.Resource.Type != "HttpRequest" {
		return nil, errors.New("invalid resource type")
	}

	return nil, errors.ErrUnsupported
}

func (s *bicepExtensionServer) Ping(ctx context.Context, req *extension.Empty) (*extension.Empty, error) {
	return req, nil
}

type HttpRequest struct {
	Uri    string `json:"uri"`
	Format string `json:"format"`
}

type HttpResponse struct {
	Uri        string `json:"uri"`
	Format     string `json:"format"`
	StatusCode int    `json:"statusCode"`
	Body       string `json:"body"`
}

func validateRequest(req *HttpRequest) error {
	if req.Uri == "" {
		return errors.New("uri cannot be empty")
	}

	return nil
}

func makeRequest(req *HttpRequest) (*HttpResponse, error) {
	res, err := http.Get(req.Uri)
	if err != nil {
		return nil, err
	}

	var responseBody string
	if req.Format == "json" {
		bytes, err := io.ReadAll(res.Body)
		if err != nil {
			return nil, err
		}

		err = json.Unmarshal(bytes, &responseBody)
		if err != nil {
			return nil, err
		}
	} else if req.Format == "raw" {
		bytes, err := io.ReadAll(res.Body)
		if err != nil {
			return nil, err
		}
		responseBody = string(bytes)
	} else {
		return nil, errors.New("format option is unrecognized")
	}

	return &HttpResponse{
		Uri:        req.Uri,
		Format:     req.Format,
		StatusCode: res.StatusCode,
		Body:       responseBody,
	}, nil
}
