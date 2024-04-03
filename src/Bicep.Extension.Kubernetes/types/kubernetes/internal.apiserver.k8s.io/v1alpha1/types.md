# internal.apiserver.k8s.io @ v1alpha1

## Resource internal.apiserver.k8s.io/StorageVersion@v1alpha1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'internal.apiserver.k8s.io/v1alpha1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'StorageVersion' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: any (Required): Any object
* **status**: [IoK8SApiApiserverinternalV1Alpha1StorageVersionStatus](#iok8sapiapiserverinternalv1alpha1storageversionstatus) (Required): API server instances report the versions they can decode and the version they encode objects to when persisting objects in the backend.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiApiserverinternalV1Alpha1ServerStorageVersion
### Properties
* **apiServerID**: string: The ID of the reporting API server.
* **decodableVersions**: string[]: The API server can decode objects encoded in these versions. The encodingVersion must be included in the decodableVersions.
* **encodingVersion**: string: The API server encodes the object to this version when persisting it in the backend (e.g., etcd).

## IoK8SApiApiserverinternalV1Alpha1StorageVersionCondition
### Properties
* **lastTransitionTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **message**: string: A human readable message indicating details about the transition.
* **observedGeneration**: int: If set, this represents the .metadata.generation that the condition was set based upon.
* **reason**: string (Required): The reason for the condition's last transition.
* **status**: string (Required): Status of the condition, one of True, False, Unknown.
* **type**: string (Required): Type of the condition.

## IoK8SApiApiserverinternalV1Alpha1StorageVersionStatus
### Properties
* **commonEncodingVersion**: string: If all API server instances agree on the same encoding storage version, then this field is set to that version. Otherwise this field is left empty. API servers should finish updating its storageVersionStatus entry before serving write operations, so that this field will be in sync with the reality.
* **conditions**: [IoK8SApiApiserverinternalV1Alpha1StorageVersionCondition](#iok8sapiapiserverinternalv1alpha1storageversioncondition)[]: The latest available observations of the storageVersion's state.
* **storageVersions**: [IoK8SApiApiserverinternalV1Alpha1ServerStorageVersion](#iok8sapiapiserverinternalv1alpha1serverstorageversion)[]: The reported versions per API server instance.

## labels
### Properties
### Additional Properties
* **Additional Properties Type**: string

## metadata
### Properties
* **annotations**: [annotations](#annotations): The annotations for the resource.
* **labels**: [labels](#labels): The labels for the resource.
* **name**: string (Required, DeployTimeConstant): The name of the resource.

