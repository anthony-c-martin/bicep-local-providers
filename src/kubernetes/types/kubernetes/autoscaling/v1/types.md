# autoscaling @ v1

## Resource autoscaling/HorizontalPodAutoscaler@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'autoscaling/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'HorizontalPodAutoscaler' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiAutoscalingV1HorizontalPodAutoscalerSpec](#iok8sapiautoscalingv1horizontalpodautoscalerspec): specification of a horizontal pod autoscaler.
* **status**: [IoK8SApiAutoscalingV1HorizontalPodAutoscalerStatus](#iok8sapiautoscalingv1horizontalpodautoscalerstatus): current status of a horizontal pod autoscaler

## Resource autoscaling/Scale@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'autoscaling/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'Scale' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiAutoscalingV1ScaleSpec](#iok8sapiautoscalingv1scalespec): ScaleSpec describes the attributes of a scale subresource.
* **status**: [IoK8SApiAutoscalingV1ScaleStatus](#iok8sapiautoscalingv1scalestatus): ScaleStatus represents the current status of a scale subresource.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiAutoscalingV1CrossVersionObjectReference
### Properties
* **apiVersion**: string: API version of the referent
* **kind**: string (Required): Kind of the referent; More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds"
* **name**: string (Required): Name of the referent; More info: http://kubernetes.io/docs/user-guide/identifiers#names

## IoK8SApiAutoscalingV1HorizontalPodAutoscalerSpec
### Properties
* **maxReplicas**: int (Required): upper limit for the number of pods that can be set by the autoscaler; cannot be smaller than MinReplicas.
* **minReplicas**: int: minReplicas is the lower limit for the number of replicas to which the autoscaler can scale down.  It defaults to 1 pod.  minReplicas is allowed to be 0 if the alpha feature gate HPAScaleToZero is enabled and at least one Object or External metric is configured.  Scaling is active as long as at least one metric value is available.
* **scaleTargetRef**: [IoK8SApiAutoscalingV1CrossVersionObjectReference](#iok8sapiautoscalingv1crossversionobjectreference) (Required): CrossVersionObjectReference contains enough information to let you identify the referred resource.
* **targetCPUUtilizationPercentage**: int: target average CPU utilization (represented as a percentage of requested CPU) over all the pods; if not specified the default autoscaling policy will be used.

## IoK8SApiAutoscalingV1HorizontalPodAutoscalerStatus
### Properties
* **currentCPUUtilizationPercentage**: int: current average CPU utilization over all pods, represented as a percentage of requested CPU, e.g. 70 means that an average pod is using now 70% of its requested CPU.
* **currentReplicas**: int (Required): current number of replicas of pods managed by this autoscaler.
* **desiredReplicas**: int (Required): desired number of replicas of pods managed by this autoscaler.
* **lastScaleTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **observedGeneration**: int: most recent generation observed by this autoscaler.

## IoK8SApiAutoscalingV1ScaleSpec
### Properties
* **replicas**: int: desired number of instances for the scaled object.

## IoK8SApiAutoscalingV1ScaleStatus
### Properties
* **replicas**: int (Required): actual number of observed instances of the scaled object.
* **selector**: string: label query over pods that should match the replicas count. This is same as the label selector but in the string format to avoid introspection by clients. The string will be in the same format as the query-param syntax. More info about label selectors: http://kubernetes.io/docs/user-guide/labels#label-selectors

## labels
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

## metadata
### Properties
* **annotations**: [annotations](#annotations): The annotations for the resource.
* **labels**: [labels](#labels): The labels for the resource.
* **name**: string (Required, DeployTimeConstant): The name of the resource.
* **namespace**: string (DeployTimeConstant): The namespace of the resource.

