# discovery.k8s.io @ v1

## Resource discovery.k8s.io/EndpointSlice@v1
* **Valid Scope(s)**: Unknown
### Properties
* **addressType**: string (Required): addressType specifies the type of address carried by this EndpointSlice. All addresses in this slice must be the same type. This field is immutable after creation. The following address types are currently supported: * IPv4: Represents an IPv4 Address. * IPv6: Represents an IPv6 Address. * FQDN: Represents a Fully Qualified Domain Name.
* **apiVersion**: 'discovery.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **endpoints**: [IoK8SApiDiscoveryV1Endpoint](#iok8sapidiscoveryv1endpoint)[] (Required): endpoints is a list of unique endpoints in this slice. Each slice may include a maximum of 1000 endpoints.
* **kind**: 'EndpointSlice' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **ports**: [IoK8SApiDiscoveryV1EndpointPort](#iok8sapidiscoveryv1endpointport)[]: ports specifies the list of network ports exposed by each endpoint in this slice. Each port must have a unique name. When ports is empty, it indicates that there are no defined ports. When a port is defined with a nil port value, it indicates "all ports". Each slice may include a maximum of 100 ports.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1ObjectReference
### Properties
* **apiVersion**: string: API version of the referent.
* **fieldPath**: string: If referring to a piece of an object instead of an entire object, this string should contain a valid JSON/Go field access statement, such as desiredState.manifest.containers[2]. For example, if the object reference is to a container within a pod, this would take on a value like: "spec.containers{name}" (where "name" refers to the name of the container that triggered the event) or if no container name is specified "spec.containers[2]" (container with index 2 in this pod). This syntax is chosen only to have some well-defined way of referencing a part of an object.
* **kind**: string: Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **namespace**: string: Namespace of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
* **resourceVersion**: string: Specific resourceVersion to which this reference is made, if any. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#concurrency-control-and-consistency
* **uid**: string: UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#uids

## IoK8SApiDiscoveryV1Endpoint
### Properties
* **addresses**: string[] (Required): addresses of this endpoint. The contents of this field are interpreted according to the corresponding EndpointSlice addressType field. Consumers must handle different types of addresses in the context of their own capabilities. This must contain at least one address but no more than 100. These are all assumed to be fungible and clients may choose to only use the first element. Refer to: https://issue.k8s.io/106267
* **conditions**: [IoK8SApiDiscoveryV1EndpointConditions](#iok8sapidiscoveryv1endpointconditions): EndpointConditions represents the current condition of an endpoint.
* **deprecatedTopology**: [IoK8SApiDiscoveryV1EndpointDeprecatedTopology](#iok8sapidiscoveryv1endpointdeprecatedtopology): deprecatedTopology contains topology information part of the v1beta1 API. This field is deprecated, and will be removed when the v1beta1 API is removed (no sooner than kubernetes v1.24).  While this field can hold values, it is not writable through the v1 API, and any attempts to write to it will be silently ignored. Topology information can be found in the zone and nodeName fields instead.
* **hints**: [IoK8SApiDiscoveryV1EndpointHints](#iok8sapidiscoveryv1endpointhints): EndpointHints provides hints describing how an endpoint should be consumed.
* **hostname**: string: hostname of this endpoint. This field may be used by consumers of endpoints to distinguish endpoints from each other (e.g. in DNS names). Multiple endpoints which use the same hostname should be considered fungible (e.g. multiple A values in DNS). Must be lowercase and pass DNS Label (RFC 1123) validation.
* **nodeName**: string: nodeName represents the name of the Node hosting this endpoint. This can be used to determine endpoints local to a Node.
* **targetRef**: [IoK8SApiCoreV1ObjectReference](#iok8sapicorev1objectreference): ObjectReference contains enough information to let you inspect or modify the referred object.
* **zone**: string: zone is the name of the Zone this endpoint exists in.

## IoK8SApiDiscoveryV1EndpointConditions
### Properties
* **ready**: bool: ready indicates that this endpoint is prepared to receive traffic, according to whatever system is managing the endpoint. A nil value indicates an unknown state. In most cases consumers should interpret this unknown state as ready. For compatibility reasons, ready should never be "true" for terminating endpoints.
* **serving**: bool: serving is identical to ready except that it is set regardless of the terminating state of endpoints. This condition should be set to true for a ready endpoint that is terminating. If nil, consumers should defer to the ready condition. This field can be enabled with the EndpointSliceTerminatingCondition feature gate.
* **terminating**: bool: terminating indicates that this endpoint is terminating. A nil value indicates an unknown state. Consumers should interpret this unknown state to mean that the endpoint is not terminating. This field can be enabled with the EndpointSliceTerminatingCondition feature gate.

## IoK8SApiDiscoveryV1EndpointDeprecatedTopology
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiDiscoveryV1EndpointHints
### Properties
* **forZones**: [IoK8SApiDiscoveryV1ForZone](#iok8sapidiscoveryv1forzone)[]: forZones indicates the zone(s) this endpoint should be consumed by to enable topology aware routing.

## IoK8SApiDiscoveryV1EndpointPort
### Properties
* **appProtocol**: string: The application protocol for this port. This field follows standard Kubernetes label syntax. Un-prefixed names are reserved for IANA standard service names (as per RFC-6335 and https://www.iana.org/assignments/service-names). Non-standard protocols should use prefixed names such as mycompany.com/my-custom-protocol.
* **name**: string: The name of this port. All ports in an EndpointSlice must have a unique name. If the EndpointSlice is dervied from a Kubernetes service, this corresponds to the Service.ports[].name. Name must either be an empty string or pass DNS_LABEL validation: * must be no more than 63 characters long. * must consist of lower case alphanumeric characters or '-'. * must start and end with an alphanumeric character. Default is empty string.
* **port**: int: The port number of the endpoint. If this is not specified, ports are not restricted and must be interpreted in the context of the specific consumer.
* **protocol**: string: The IP protocol for this port. Must be UDP, TCP, or SCTP. Default is TCP.

## IoK8SApiDiscoveryV1ForZone
### Properties
* **name**: string (Required): name represents the name of the zone.

## labels
### Properties
### Additional Properties
* **Additional Properties Type**: string

## metadata
### Properties
* **annotations**: [annotations](#annotations): The annotations for the resource.
* **labels**: [labels](#labels): The labels for the resource.
* **name**: string (Required, DeployTimeConstant): The name of the resource.
* **namespace**: string (DeployTimeConstant): The namespace of the resource.

