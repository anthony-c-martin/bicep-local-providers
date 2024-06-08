# rbac.authorization.k8s.io @ v1

## Resource rbac.authorization.k8s.io/ClusterRole@v1
* **Valid Scope(s)**: Unknown
### Properties
* **aggregationRule**: [IoK8SApiRbacV1AggregationRule](#iok8sapirbacv1aggregationrule): AggregationRule describes how to locate ClusterRoles to aggregate into the ClusterRole
* **apiVersion**: 'rbac.authorization.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'ClusterRole' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **rules**: [IoK8SApiRbacV1PolicyRule](#iok8sapirbacv1policyrule)[]: Rules holds all the PolicyRules for this ClusterRole

## Resource rbac.authorization.k8s.io/ClusterRoleBinding@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'rbac.authorization.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'ClusterRoleBinding' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **roleRef**: [IoK8SApiRbacV1RoleRef](#iok8sapirbacv1roleref) (Required): RoleRef contains information that points to the role being used
* **subjects**: [IoK8SApiRbacV1Subject](#iok8sapirbacv1subject)[]: Subjects holds references to the objects the role applies to.

## Resource rbac.authorization.k8s.io/Role@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'rbac.authorization.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'Role' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **rules**: [IoK8SApiRbacV1PolicyRule](#iok8sapirbacv1policyrule)[]: Rules holds all the PolicyRules for this Role

## Resource rbac.authorization.k8s.io/RoleBinding@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'rbac.authorization.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'RoleBinding' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **roleRef**: [IoK8SApiRbacV1RoleRef](#iok8sapirbacv1roleref) (Required): RoleRef contains information that points to the role being used
* **subjects**: [IoK8SApiRbacV1Subject](#iok8sapirbacv1subject)[]: Subjects holds references to the objects the role applies to.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

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

## IoK8SApiRbacV1AggregationRule
### Properties
* **clusterRoleSelectors**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector)[]: ClusterRoleSelectors holds a list of selectors which will be used to find ClusterRoles and create the rules. If any of the selectors match, then the ClusterRole's permissions will be added

## IoK8SApiRbacV1PolicyRule
### Properties
* **apiGroups**: string[]: APIGroups is the name of the APIGroup that contains the resources.  If multiple API groups are specified, any action requested against one of the enumerated resources in any API group will be allowed. "" represents the core API group and "*" represents all API groups.
* **nonResourceURLs**: string[]: NonResourceURLs is a set of partial urls that a user should have access to.  *s are allowed, but only as the full, final step in the path Since non-resource URLs are not namespaced, this field is only applicable for ClusterRoles referenced from a ClusterRoleBinding. Rules can either apply to API resources (such as "pods" or "secrets") or non-resource URL paths (such as "/api"),  but not both.
* **resourceNames**: string[]: ResourceNames is an optional white list of names that the rule applies to.  An empty set means that everything is allowed.
* **resources**: string[]: Resources is a list of resources this rule applies to. '*' represents all resources.
* **verbs**: string[] (Required): Verbs is a list of Verbs that apply to ALL the ResourceKinds contained in this rule. '*' represents all verbs.

## IoK8SApiRbacV1RoleRef
### Properties
* **apiGroup**: string (Required): APIGroup is the group for the resource being referenced
* **kind**: string (Required): Kind is the type of resource being referenced
* **name**: string (Required): Name is the name of resource being referenced

## IoK8SApiRbacV1Subject
### Properties
* **apiGroup**: string: APIGroup holds the API group of the referenced subject. Defaults to "" for ServiceAccount subjects. Defaults to "rbac.authorization.k8s.io" for User and Group subjects.
* **kind**: string (Required): Kind of object being referenced. Values defined by this API group are "User", "Group", and "ServiceAccount". If the Authorizer does not recognized the kind value, the Authorizer should report an error.
* **name**: string (Required): Name of the object being referenced.
* **namespace**: string: Namespace of the referenced object.  If the object kind is non-namespace, such as "User" or "Group", and this value is not empty the Authorizer should report an error.

## labels
### Properties
### Additional Properties
* **Additional Properties Type**: string

## labels
### Properties
### Additional Properties
* **Additional Properties Type**: string

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

