# flowcontrol.apiserver.k8s.io @ v1beta2

## Resource flowcontrol.apiserver.k8s.io/FlowSchema@v1beta2
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'flowcontrol.apiserver.k8s.io/v1beta2' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'FlowSchema' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiFlowcontrolV1Beta2FlowSchemaSpec](#iok8sapiflowcontrolv1beta2flowschemaspec): FlowSchemaSpec describes how the FlowSchema's specification looks like.
* **status**: [IoK8SApiFlowcontrolV1Beta2FlowSchemaStatus](#iok8sapiflowcontrolv1beta2flowschemastatus): FlowSchemaStatus represents the current state of a FlowSchema.

## Resource flowcontrol.apiserver.k8s.io/PriorityLevelConfiguration@v1beta2
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'flowcontrol.apiserver.k8s.io/v1beta2' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'PriorityLevelConfiguration' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationSpec](#iok8sapiflowcontrolv1beta2prioritylevelconfigurationspec): PriorityLevelConfigurationSpec specifies the configuration of a priority level.
* **status**: [IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationStatus](#iok8sapiflowcontrolv1beta2prioritylevelconfigurationstatus): PriorityLevelConfigurationStatus represents the current state of a "request-priority".

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiFlowcontrolV1Beta2FlowDistinguisherMethod
### Properties
* **type**: string (Required): `type` is the type of flow distinguisher method The supported types are "ByUser" and "ByNamespace". Required.

## IoK8SApiFlowcontrolV1Beta2FlowSchemaCondition
### Properties
* **lastTransitionTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **message**: string: `message` is a human-readable message indicating details about last transition.
* **reason**: string: `reason` is a unique, one-word, CamelCase reason for the condition's last transition.
* **status**: string: `status` is the status of the condition. Can be True, False, Unknown. Required.
* **type**: string: `type` is the type of the condition. Required.

## IoK8SApiFlowcontrolV1Beta2FlowSchemaSpec
### Properties
* **distinguisherMethod**: [IoK8SApiFlowcontrolV1Beta2FlowDistinguisherMethod](#iok8sapiflowcontrolv1beta2flowdistinguishermethod): FlowDistinguisherMethod specifies the method of a flow distinguisher.
* **matchingPrecedence**: int: `matchingPrecedence` is used to choose among the FlowSchemas that match a given request. The chosen FlowSchema is among those with the numerically lowest (which we take to be logically highest) MatchingPrecedence.  Each MatchingPrecedence value must be ranged in [1,10000]. Note that if the precedence is not specified, it will be set to 1000 as default.
* **priorityLevelConfiguration**: [IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationReference](#iok8sapiflowcontrolv1beta2prioritylevelconfigurationreference) (Required): PriorityLevelConfigurationReference contains information that points to the "request-priority" being used.
* **rules**: [IoK8SApiFlowcontrolV1Beta2PolicyRulesWithSubjects](#iok8sapiflowcontrolv1beta2policyruleswithsubjects)[]: `rules` describes which requests will match this flow schema. This FlowSchema matches a request if and only if at least one member of rules matches the request. if it is an empty slice, there will be no requests matching the FlowSchema.

## IoK8SApiFlowcontrolV1Beta2FlowSchemaStatus
### Properties
* **conditions**: [IoK8SApiFlowcontrolV1Beta2FlowSchemaCondition](#iok8sapiflowcontrolv1beta2flowschemacondition)[]: `conditions` is a list of the current states of FlowSchema.

## IoK8SApiFlowcontrolV1Beta2GroupSubject
### Properties
* **name**: string (Required): name is the user group that matches, or "*" to match all user groups. See https://github.com/kubernetes/apiserver/blob/master/pkg/authentication/user/user.go for some well-known group names. Required.

## IoK8SApiFlowcontrolV1Beta2LimitedPriorityLevelConfiguration
### Properties
* **assuredConcurrencyShares**: int: `assuredConcurrencyShares` (ACS) configures the execution limit, which is a limit on the number of requests of this priority level that may be exeucting at a given time.  ACS must be a positive number. The server's concurrency limit (SCL) is divided among the concurrency-controlled priority levels in proportion to their assured concurrency shares. This produces the assured concurrency value (ACV) --- the number of requests that may be executing at a time --- for each such priority level:

            ACV(l) = ceil( SCL * ACS(l) / ( sum[priority levels k] ACS(k) ) )

bigger numbers of ACS mean more reserved concurrent requests (at the expense of every other PL). This field has a default value of 30.
* **limitResponse**: [IoK8SApiFlowcontrolV1Beta2LimitResponse](#iok8sapiflowcontrolv1beta2limitresponse): LimitResponse defines how to handle requests that can not be executed right now.

## IoK8SApiFlowcontrolV1Beta2LimitResponse
### Properties
* **queuing**: [IoK8SApiFlowcontrolV1Beta2QueuingConfiguration](#iok8sapiflowcontrolv1beta2queuingconfiguration): QueuingConfiguration holds the configuration parameters for queuing
* **type**: string (Required): `type` is "Queue" or "Reject". "Queue" means that requests that can not be executed upon arrival are held in a queue until they can be executed or a queuing limit is reached. "Reject" means that requests that can not be executed upon arrival are rejected. Required.

## IoK8SApiFlowcontrolV1Beta2NonResourcePolicyRule
### Properties
* **nonResourceURLs**: string[] (Required): `nonResourceURLs` is a set of url prefixes that a user should have access to and may not be empty. For example:
  - "/healthz" is legal
  - "/hea*" is illegal
  - "/hea" is legal but matches nothing
  - "/hea/*" also matches nothing
  - "/healthz/*" matches all per-component health checks.
"*" matches all non-resource urls. if it is present, it must be the only entry. Required.
* **verbs**: string[] (Required): `verbs` is a list of matching verbs and may not be empty. "*" matches all verbs. If it is present, it must be the only entry. Required.

## IoK8SApiFlowcontrolV1Beta2PolicyRulesWithSubjects
### Properties
* **nonResourceRules**: [IoK8SApiFlowcontrolV1Beta2NonResourcePolicyRule](#iok8sapiflowcontrolv1beta2nonresourcepolicyrule)[]: `nonResourceRules` is a list of NonResourcePolicyRules that identify matching requests according to their verb and the target non-resource URL.
* **resourceRules**: [IoK8SApiFlowcontrolV1Beta2ResourcePolicyRule](#iok8sapiflowcontrolv1beta2resourcepolicyrule)[]: `resourceRules` is a slice of ResourcePolicyRules that identify matching requests according to their verb and the target resource. At least one of `resourceRules` and `nonResourceRules` has to be non-empty.
* **subjects**: [IoK8SApiFlowcontrolV1Beta2Subject](#iok8sapiflowcontrolv1beta2subject)[] (Required): subjects is the list of normal user, serviceaccount, or group that this rule cares about. There must be at least one member in this slice. A slice that includes both the system:authenticated and system:unauthenticated user groups matches every request. Required.

## IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationCondition
### Properties
* **lastTransitionTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **message**: string: `message` is a human-readable message indicating details about last transition.
* **reason**: string: `reason` is a unique, one-word, CamelCase reason for the condition's last transition.
* **status**: string: `status` is the status of the condition. Can be True, False, Unknown. Required.
* **type**: string: `type` is the type of the condition. Required.

## IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationReference
### Properties
* **name**: string (Required): `name` is the name of the priority level configuration being referenced Required.

## IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationSpec
### Properties
* **limited**: [IoK8SApiFlowcontrolV1Beta2LimitedPriorityLevelConfiguration](#iok8sapiflowcontrolv1beta2limitedprioritylevelconfiguration): LimitedPriorityLevelConfiguration specifies how to handle requests that are subject to limits. It addresses two issues:
  - How are requests for this priority level limited?
  - What should be done with requests that exceed the limit?
* **type**: string (Required): `type` indicates whether this priority level is subject to limitation on request execution.  A value of `"Exempt"` means that requests of this priority level are not subject to a limit (and thus are never queued) and do not detract from the capacity made available to other priority levels.  A value of `"Limited"` means that (a) requests of this priority level _are_ subject to limits and (b) some of the server's limited capacity is made available exclusively to this priority level. Required.

## IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationStatus
### Properties
* **conditions**: [IoK8SApiFlowcontrolV1Beta2PriorityLevelConfigurationCondition](#iok8sapiflowcontrolv1beta2prioritylevelconfigurationcondition)[]: `conditions` is the current state of "request-priority".

## IoK8SApiFlowcontrolV1Beta2QueuingConfiguration
### Properties
* **handSize**: int: `handSize` is a small positive number that configures the shuffle sharding of requests into queues.  When enqueuing a request at this priority level the request's flow identifier (a string pair) is hashed and the hash value is used to shuffle the list of queues and deal a hand of the size specified here.  The request is put into one of the shortest queues in that hand. `handSize` must be no larger than `queues`, and should be significantly smaller (so that a few heavy flows do not saturate most of the queues).  See the user-facing documentation for more extensive guidance on setting this field.  This field has a default value of 8.
* **queueLengthLimit**: int: `queueLengthLimit` is the maximum number of requests allowed to be waiting in a given queue of this priority level at a time; excess requests are rejected.  This value must be positive.  If not specified, it will be defaulted to 50.
* **queues**: int: `queues` is the number of queues for this priority level. The queues exist independently at each apiserver. The value must be positive.  Setting it to 1 effectively precludes shufflesharding and thus makes the distinguisher method of associated flow schemas irrelevant.  This field has a default value of 64.

## IoK8SApiFlowcontrolV1Beta2ResourcePolicyRule
### Properties
* **apiGroups**: string[] (Required): `apiGroups` is a list of matching API groups and may not be empty. "*" matches all API groups and, if present, must be the only entry. Required.
* **clusterScope**: bool: `clusterScope` indicates whether to match requests that do not specify a namespace (which happens either because the resource is not namespaced or the request targets all namespaces). If this field is omitted or false then the `namespaces` field must contain a non-empty list.
* **namespaces**: string[]: `namespaces` is a list of target namespaces that restricts matches.  A request that specifies a target namespace matches only if either (a) this list contains that target namespace or (b) this list contains "*".  Note that "*" matches any specified namespace but does not match a request that _does not specify_ a namespace (see the `clusterScope` field for that). This list may be empty, but only if `clusterScope` is true.
* **resources**: string[] (Required): `resources` is a list of matching resources (i.e., lowercase and plural) with, if desired, subresource.  For example, [ "services", "nodes/status" ].  This list may not be empty. "*" matches all resources and, if present, must be the only entry. Required.
* **verbs**: string[] (Required): `verbs` is a list of matching verbs and may not be empty. "*" matches all verbs and, if present, must be the only entry. Required.

## IoK8SApiFlowcontrolV1Beta2ServiceAccountSubject
### Properties
* **name**: string (Required): `name` is the name of matching ServiceAccount objects, or "*" to match regardless of name. Required.
* **namespace**: string (Required): `namespace` is the namespace of matching ServiceAccount objects. Required.

## IoK8SApiFlowcontrolV1Beta2Subject
### Properties
* **group**: [IoK8SApiFlowcontrolV1Beta2GroupSubject](#iok8sapiflowcontrolv1beta2groupsubject): GroupSubject holds detailed information for group-kind subject.
* **kind**: string (Required): `kind` indicates which one of the other fields is non-empty. Required
* **serviceAccount**: [IoK8SApiFlowcontrolV1Beta2ServiceAccountSubject](#iok8sapiflowcontrolv1beta2serviceaccountsubject): ServiceAccountSubject holds detailed information for service-account-kind subject.
* **user**: [IoK8SApiFlowcontrolV1Beta2UserSubject](#iok8sapiflowcontrolv1beta2usersubject): UserSubject holds detailed information for user-kind subject.

## IoK8SApiFlowcontrolV1Beta2UserSubject
### Properties
* **name**: string (Required): `name` is the username that matches, or "*" to match all usernames. Required.

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

## metadata
### Properties
* **annotations**: [annotations](#annotations): The annotations for the resource.
* **labels**: [labels](#labels): The labels for the resource.
* **name**: string (Required, DeployTimeConstant): The name of the resource.

