# scheduling.k8s.io @ v1

## Resource scheduling.k8s.io/PriorityClass@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'scheduling.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **description**: string: description is an arbitrary string that usually provides guidelines on when this priority class should be used.
* **globalDefault**: bool: globalDefault specifies whether this PriorityClass should be considered as the default priority for pods that do not have any priority class. Only one PriorityClass can be marked as `globalDefault`. However, if more than one PriorityClasses exists with their `globalDefault` field set to true, the smallest value of such global default PriorityClasses will be used as the default priority.
* **kind**: 'PriorityClass' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **preemptionPolicy**: string: PreemptionPolicy is the Policy for preempting pods with lower priority. One of Never, PreemptLowerPriority. Defaults to PreemptLowerPriority if unset.
* **value**: int (Required): The value of this priority class. This is the actual priority that pods receive when they have the name of this class in their pod spec.

## annotations
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

