# policy @ v1

## Resource policy/PodDisruptionBudget@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'policy/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'PodDisruptionBudget' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiPolicyV1PodDisruptionBudgetSpec](#iok8sapipolicyv1poddisruptionbudgetspec): PodDisruptionBudgetSpec is a description of a PodDisruptionBudget.
* **status**: [IoK8SApiPolicyV1PodDisruptionBudgetStatus](#iok8sapipolicyv1poddisruptionbudgetstatus): PodDisruptionBudgetStatus represents information about the status of a PodDisruptionBudget. Status may trail the actual state of a system.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApimachineryPkgApisMetaV1Condition
### Properties
* **lastTransitionTime**: string (Required): Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **message**: string (Required): message is a human readable message indicating details about the transition. This may be an empty string.
* **observedGeneration**: int: observedGeneration represents the .metadata.generation that the condition was set based upon. For instance, if .metadata.generation is currently 12, but the .status.conditions[x].observedGeneration is 9, the condition is out of date with respect to the current state of the instance.
* **reason**: string (Required): reason contains a programmatic identifier indicating the reason for the condition's last transition. Producers of specific condition types may define expected values and meanings for this field, and whether the values are considered a guaranteed API. The value should be a CamelCase string. This field may not be empty.
* **status**: string (Required): status of the condition, one of True, False, Unknown.
* **type**: string (Required): type of condition in CamelCase or in foo.example.com/CamelCase.

## IoK8SApimachineryPkgApisMetaV1LabelSelector
### Properties
* **matchExpressions**: [IoK8SApimachineryPkgApisMetaV1LabelSelectorRequirement](#iok8sapimachinerypkgapismetav1labelselectorrequirement)[]: matchExpressions is a list of label selector requirements. The requirements are ANDed.
* **matchLabels**: [IoK8SApimachineryPkgApisMetaV1LabelSelectorMatchLabels](#iok8sapimachinerypkgapismetav1labelselectormatchlabels): matchLabels is a map of {key,value} pairs. A single {key,value} in the matchLabels map is equivalent to an element of matchExpressions, whose key field is "key", the operator is "In", and the values array contains only "value". The requirements are ANDed.

## IoK8SApimachineryPkgApisMetaV1LabelSelectorMatchLabels
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApimachineryPkgApisMetaV1LabelSelectorRequirement
### Properties
* **key**: string (Required): key is the label key that the selector applies to.
* **operator**: string (Required): operator represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists and DoesNotExist.
* **values**: string[]: values is an array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. This array is replaced during a strategic merge patch.

## IoK8SApiPolicyV1PodDisruptionBudgetSpec
### Properties
* **maxUnavailable**: string: IntOrString is a type that can hold an int32 or a string.  When used in JSON or YAML marshalling and unmarshalling, it produces or consumes the inner type.  This allows you to have, for example, a JSON field that can accept a name or number.
* **minAvailable**: string: IntOrString is a type that can hold an int32 or a string.  When used in JSON or YAML marshalling and unmarshalling, it produces or consumes the inner type.  This allows you to have, for example, a JSON field that can accept a name or number.
* **selector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.

## IoK8SApiPolicyV1PodDisruptionBudgetStatus
### Properties
* **conditions**: [IoK8SApimachineryPkgApisMetaV1Condition](#iok8sapimachinerypkgapismetav1condition)[]: Conditions contain conditions for PDB. The disruption controller sets the DisruptionAllowed condition. The following are known values for the reason field (additional reasons could be added in the future): - SyncFailed: The controller encountered an error and wasn't able to compute
              the number of allowed disruptions. Therefore no disruptions are
              allowed and the status of the condition will be False.
- InsufficientPods: The number of pods are either at or below the number
                    required by the PodDisruptionBudget. No disruptions are
                    allowed and the status of the condition will be False.
- SufficientPods: There are more pods than required by the PodDisruptionBudget.
                  The condition will be True, and the number of allowed
                  disruptions are provided by the disruptionsAllowed property.
* **currentHealthy**: int (Required): current number of healthy pods
* **desiredHealthy**: int (Required): minimum desired number of healthy pods
* **disruptedPods**: [IoK8SApiPolicyV1PodDisruptionBudgetStatusDisruptedPods](#iok8sapipolicyv1poddisruptionbudgetstatusdisruptedpods): DisruptedPods contains information about pods whose eviction was processed by the API server eviction subresource handler but has not yet been observed by the PodDisruptionBudget controller. A pod will be in this map from the time when the API server processed the eviction request to the time when the pod is seen by PDB controller as having been marked for deletion (or after a timeout). The key in the map is the name of the pod and the value is the time when the API server processed the eviction request. If the deletion didn't occur and a pod is still there it will be removed from the list automatically by PodDisruptionBudget controller after some time. If everything goes smooth this map should be empty for the most of the time. Large number of entries in the map may indicate problems with pod deletions.
* **disruptionsAllowed**: int (Required): Number of pod disruptions that are currently allowed.
* **expectedPods**: int (Required): total number of pods counted by this disruption budget
* **observedGeneration**: int: Most recent generation observed when updating this PDB status. DisruptionsAllowed and other status information is valid only if observedGeneration equals to PDB's object generation.

## IoK8SApiPolicyV1PodDisruptionBudgetStatusDisruptedPods
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
* **namespace**: string (DeployTimeConstant): The namespace of the resource.

