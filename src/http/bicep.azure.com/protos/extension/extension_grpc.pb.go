// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.3.0
// - protoc             v5.27.0
// source: src/extension.proto

package bicep_azure_com

import (
	context "context"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

const (
	BicepExtension_CreateOrUpdate_FullMethodName = "/extension.BicepExtension/CreateOrUpdate"
	BicepExtension_Preview_FullMethodName        = "/extension.BicepExtension/Preview"
	BicepExtension_Get_FullMethodName            = "/extension.BicepExtension/Get"
	BicepExtension_Delete_FullMethodName         = "/extension.BicepExtension/Delete"
	BicepExtension_Ping_FullMethodName           = "/extension.BicepExtension/Ping"
)

// BicepExtensionClient is the client API for BicepExtension service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type BicepExtensionClient interface {
	CreateOrUpdate(ctx context.Context, in *ResourceSpecification, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error)
	Preview(ctx context.Context, in *ResourceSpecification, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error)
	Get(ctx context.Context, in *ResourceReference, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error)
	Delete(ctx context.Context, in *ResourceReference, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error)
	Ping(ctx context.Context, in *Empty, opts ...grpc.CallOption) (*Empty, error)
}

type bicepExtensionClient struct {
	cc grpc.ClientConnInterface
}

func NewBicepExtensionClient(cc grpc.ClientConnInterface) BicepExtensionClient {
	return &bicepExtensionClient{cc}
}

func (c *bicepExtensionClient) CreateOrUpdate(ctx context.Context, in *ResourceSpecification, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error) {
	out := new(LocalExtensibilityOperationResponse)
	err := c.cc.Invoke(ctx, BicepExtension_CreateOrUpdate_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *bicepExtensionClient) Preview(ctx context.Context, in *ResourceSpecification, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error) {
	out := new(LocalExtensibilityOperationResponse)
	err := c.cc.Invoke(ctx, BicepExtension_Preview_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *bicepExtensionClient) Get(ctx context.Context, in *ResourceReference, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error) {
	out := new(LocalExtensibilityOperationResponse)
	err := c.cc.Invoke(ctx, BicepExtension_Get_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *bicepExtensionClient) Delete(ctx context.Context, in *ResourceReference, opts ...grpc.CallOption) (*LocalExtensibilityOperationResponse, error) {
	out := new(LocalExtensibilityOperationResponse)
	err := c.cc.Invoke(ctx, BicepExtension_Delete_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *bicepExtensionClient) Ping(ctx context.Context, in *Empty, opts ...grpc.CallOption) (*Empty, error) {
	out := new(Empty)
	err := c.cc.Invoke(ctx, BicepExtension_Ping_FullMethodName, in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// BicepExtensionServer is the server API for BicepExtension service.
// All implementations must embed UnimplementedBicepExtensionServer
// for forward compatibility
type BicepExtensionServer interface {
	CreateOrUpdate(context.Context, *ResourceSpecification) (*LocalExtensibilityOperationResponse, error)
	Preview(context.Context, *ResourceSpecification) (*LocalExtensibilityOperationResponse, error)
	Get(context.Context, *ResourceReference) (*LocalExtensibilityOperationResponse, error)
	Delete(context.Context, *ResourceReference) (*LocalExtensibilityOperationResponse, error)
	Ping(context.Context, *Empty) (*Empty, error)
	mustEmbedUnimplementedBicepExtensionServer()
}

// UnimplementedBicepExtensionServer must be embedded to have forward compatible implementations.
type UnimplementedBicepExtensionServer struct {
}

func (UnimplementedBicepExtensionServer) CreateOrUpdate(context.Context, *ResourceSpecification) (*LocalExtensibilityOperationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method CreateOrUpdate not implemented")
}
func (UnimplementedBicepExtensionServer) Preview(context.Context, *ResourceSpecification) (*LocalExtensibilityOperationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Preview not implemented")
}
func (UnimplementedBicepExtensionServer) Get(context.Context, *ResourceReference) (*LocalExtensibilityOperationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Get not implemented")
}
func (UnimplementedBicepExtensionServer) Delete(context.Context, *ResourceReference) (*LocalExtensibilityOperationResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Delete not implemented")
}
func (UnimplementedBicepExtensionServer) Ping(context.Context, *Empty) (*Empty, error) {
	return nil, status.Errorf(codes.Unimplemented, "method Ping not implemented")
}
func (UnimplementedBicepExtensionServer) mustEmbedUnimplementedBicepExtensionServer() {}

// UnsafeBicepExtensionServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to BicepExtensionServer will
// result in compilation errors.
type UnsafeBicepExtensionServer interface {
	mustEmbedUnimplementedBicepExtensionServer()
}

func RegisterBicepExtensionServer(s grpc.ServiceRegistrar, srv BicepExtensionServer) {
	s.RegisterService(&BicepExtension_ServiceDesc, srv)
}

func _BicepExtension_CreateOrUpdate_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ResourceSpecification)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(BicepExtensionServer).CreateOrUpdate(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: BicepExtension_CreateOrUpdate_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(BicepExtensionServer).CreateOrUpdate(ctx, req.(*ResourceSpecification))
	}
	return interceptor(ctx, in, info, handler)
}

func _BicepExtension_Preview_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ResourceSpecification)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(BicepExtensionServer).Preview(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: BicepExtension_Preview_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(BicepExtensionServer).Preview(ctx, req.(*ResourceSpecification))
	}
	return interceptor(ctx, in, info, handler)
}

func _BicepExtension_Get_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ResourceReference)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(BicepExtensionServer).Get(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: BicepExtension_Get_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(BicepExtensionServer).Get(ctx, req.(*ResourceReference))
	}
	return interceptor(ctx, in, info, handler)
}

func _BicepExtension_Delete_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(ResourceReference)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(BicepExtensionServer).Delete(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: BicepExtension_Delete_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(BicepExtensionServer).Delete(ctx, req.(*ResourceReference))
	}
	return interceptor(ctx, in, info, handler)
}

func _BicepExtension_Ping_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(Empty)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(BicepExtensionServer).Ping(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: BicepExtension_Ping_FullMethodName,
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(BicepExtensionServer).Ping(ctx, req.(*Empty))
	}
	return interceptor(ctx, in, info, handler)
}

// BicepExtension_ServiceDesc is the grpc.ServiceDesc for BicepExtension service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var BicepExtension_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "extension.BicepExtension",
	HandlerType: (*BicepExtensionServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "CreateOrUpdate",
			Handler:    _BicepExtension_CreateOrUpdate_Handler,
		},
		{
			MethodName: "Preview",
			Handler:    _BicepExtension_Preview_Handler,
		},
		{
			MethodName: "Get",
			Handler:    _BicepExtension_Get_Handler,
		},
		{
			MethodName: "Delete",
			Handler:    _BicepExtension_Delete_Handler,
		},
		{
			MethodName: "Ping",
			Handler:    _BicepExtension_Ping_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "src/extension.proto",
}
