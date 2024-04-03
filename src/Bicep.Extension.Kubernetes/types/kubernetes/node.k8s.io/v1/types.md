# node.k8s.io @ v1

## Resource node.k8s.io/RuntimeClass@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'node.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **handler**: string (Required): Handler specifies the underlying runtime and configuration that the CRI implementation will use to handle pods of this class. The possible values are specific to the node & CRI configuration.  It is assumed that all handlers are available on every node, and handlers of the same name are equivalent on every node. For example, a handler called "runc" might specify that the runc OCI runtime (using native Linux containers) will be used to run the containers in a pod. The Handler must be lowercase, conform to the DNS Label (RFC 1123) requirements, and is immutable.
* **kind**: 'RuntimeClass' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **overhead**: [IoK8SApiNodeV1Overhead](#iok8sapinodev1overhead): Overhead structure represents the resource overhead associated with running a pod.
* **scheduling**: [IoK8SApiNodeV1Scheduling](#iok8sapinodev1scheduling): Scheduling specifies the scheduling constraints for nodes supporting a RuntimeClass.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1Toleration
### Properties
* **effect**: string: Effect indicates the taint effect to match. Empty means match all taint effects. When specified, allowed values are NoSchedule, PreferNoSchedule and NoExecute.
* **key**: string: Key is the taint key that the toleration applies to. Empty means match all taint keys. If the key is empty, operator must be Exists; this combination means to match all values and all keys.
* **operator**: string: Operator represents a key's relationship to the value. Valid operators are Exists and Equal. Defaults to Equal. Exists is equivalent to wildcard for value, so that a pod can tolerate all taints of a particular category.
* **tolerationSeconds**: int: TolerationSeconds represents the period of time the toleration (which must be of effect NoExecute, otherwise this field is ignored) tolerates the taint. By default, it is not set, which means tolerate the taint forever (do not evict). Zero and negative values will be treated as 0 (evict immediately) by the system.
* **value**: string: Value is the taint value the toleration matches to. If the operator is Exists, the value should be empty, otherwise just a regular string.

## IoK8SApiNodeV1Overhead
### Properties
* **podFixed**: [IoK8SApiNodeV1OverheadPodFixed](#iok8sapinodev1overheadpodfixed): PodFixed represents the fixed resource overhead associated with running a pod.

## IoK8SApiNodeV1OverheadPodFixed
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiNodeV1Scheduling
### Properties
* **nodeSelector**: [IoK8SApiNodeV1SchedulingNodeSelector](#iok8sapinodev1schedulingnodeselector): nodeSelector lists labels that must be present on nodes that support this RuntimeClass. Pods using this RuntimeClass can only be scheduled to a node matched by this selector. The RuntimeClass nodeSelector is merged with a pod's existing nodeSelector. Any conflicts will cause the pod to be rejected in admission.
* **tolerations**: [IoK8SApiCoreV1Toleration](#iok8sapicorev1toleration)[]: tolerations are appended (excluding duplicates) to pods running with this RuntimeClass during admission, effectively unioning the set of nodes tolerated by the pod and the RuntimeClass.

## IoK8SApiNodeV1SchedulingNodeSelector
### Properties
### Additional Properties
* **Additional Properties Type**: string

## labels
### Properties
### Additional Properties
* **Additional Properties Type**: string

## metadata
### Properties
* **annotations**: [annotations](#annotations): The annotations for the resource.
* **labels**: [labels](#labels): The labels for the resource.
* **name**: string (Required, DeployTimeConstant): The name of the resource.

