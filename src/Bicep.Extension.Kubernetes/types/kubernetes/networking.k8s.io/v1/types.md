# networking.k8s.io @ v1

## Resource networking.k8s.io/Ingress@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'networking.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'Ingress' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiNetworkingV1IngressSpec](#iok8sapinetworkingv1ingressspec): IngressSpec describes the Ingress the user wishes to exist.
* **status**: [IoK8SApiNetworkingV1IngressStatus](#iok8sapinetworkingv1ingressstatus): IngressStatus describe the current state of the Ingress.

## Resource networking.k8s.io/IngressClass@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'networking.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'IngressClass' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiNetworkingV1IngressClassSpec](#iok8sapinetworkingv1ingressclassspec): IngressClassSpec provides information about the class of an Ingress.

## Resource networking.k8s.io/NetworkPolicy@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'networking.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'NetworkPolicy' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiNetworkingV1NetworkPolicySpec](#iok8sapinetworkingv1networkpolicyspec): NetworkPolicySpec provides the specification of a NetworkPolicy
* **status**: [IoK8SApiNetworkingV1NetworkPolicyStatus](#iok8sapinetworkingv1networkpolicystatus): NetworkPolicyStatus describe the current state of the NetworkPolicy.

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

## IoK8SApiCoreV1LoadBalancerIngress
### Properties
* **hostname**: string: Hostname is set for load-balancer ingress points that are DNS based (typically AWS load-balancers)
* **ip**: string: IP is set for load-balancer ingress points that are IP based (typically GCE or OpenStack load-balancers)
* **ports**: [IoK8SApiCoreV1PortStatus](#iok8sapicorev1portstatus)[]: Ports is a list of records of service ports If used, every port defined in the service should have an entry in it

## IoK8SApiCoreV1LoadBalancerStatus
### Properties
* **ingress**: [IoK8SApiCoreV1LoadBalancerIngress](#iok8sapicorev1loadbalanceringress)[]: Ingress is a list containing ingress points for the load-balancer. Traffic intended for the service should be sent to these ingress points.

## IoK8SApiCoreV1PortStatus
### Properties
* **error**: string: Error is to record the problem with the service port The format of the error shall comply with the following rules: - built-in error values shall be specified in this file and those shall use
  CamelCase names
- cloud provider specific error values must have names that comply with the
  format foo.example.com/CamelCase.
* **port**: int (Required): Port is the port number of the service port of which status is recorded here
* **protocol**: string (Required): Protocol is the protocol of the service port of which status is recorded here The supported values are: "TCP", "UDP", "SCTP"

## IoK8SApiCoreV1TypedLocalObjectReference
### Properties
* **apiGroup**: string: APIGroup is the group for the resource being referenced. If APIGroup is not specified, the specified Kind must be in the core API group. For any other third-party types, APIGroup is required.
* **kind**: string (Required): Kind is the type of resource being referenced
* **name**: string (Required): Name is the name of resource being referenced

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

## IoK8SApiNetworkingV1HttpIngressPath
### Properties
* **backend**: [IoK8SApiNetworkingV1IngressBackend](#iok8sapinetworkingv1ingressbackend) (Required): IngressBackend describes all endpoints for a given service and port.
* **path**: string: Path is matched against the path of an incoming request. Currently it can contain characters disallowed from the conventional "path" part of a URL as defined by RFC 3986. Paths must begin with a '/' and must be present when using PathType with value "Exact" or "Prefix".
* **pathType**: string (Required): PathType determines the interpretation of the Path matching. PathType can be one of the following values: * Exact: Matches the URL path exactly. * Prefix: Matches based on a URL path prefix split by '/'. Matching is
  done on a path element by element basis. A path element refers is the
  list of labels in the path split by the '/' separator. A request is a
  match for path p if every p is an element-wise prefix of p of the
  request path. Note that if the last element of the path is a substring
  of the last element in request path, it is not a match (e.g. /foo/bar
  matches /foo/bar/baz, but does not match /foo/barbaz).
* ImplementationSpecific: Interpretation of the Path matching is up to
  the IngressClass. Implementations can treat this as a separate PathType
  or treat it identically to Prefix or Exact path types.
Implementations are required to support all path types.

## IoK8SApiNetworkingV1HttpIngressRuleValue
### Properties
* **paths**: [IoK8SApiNetworkingV1HttpIngressPath](#iok8sapinetworkingv1httpingresspath)[] (Required): A collection of paths that map requests to backends.

## IoK8SApiNetworkingV1IngressBackend
### Properties
* **resource**: [IoK8SApiCoreV1TypedLocalObjectReference](#iok8sapicorev1typedlocalobjectreference): TypedLocalObjectReference contains enough information to let you locate the typed referenced object inside the same namespace.
* **service**: [IoK8SApiNetworkingV1IngressServiceBackend](#iok8sapinetworkingv1ingressservicebackend): IngressServiceBackend references a Kubernetes Service as a Backend.

## IoK8SApiNetworkingV1IngressClassParametersReference
### Properties
* **apiGroup**: string: APIGroup is the group for the resource being referenced. If APIGroup is not specified, the specified Kind must be in the core API group. For any other third-party types, APIGroup is required.
* **kind**: string (Required): Kind is the type of resource being referenced.
* **name**: string (Required): Name is the name of resource being referenced.
* **namespace**: string: Namespace is the namespace of the resource being referenced. This field is required when scope is set to "Namespace" and must be unset when scope is set to "Cluster".
* **scope**: string: Scope represents if this refers to a cluster or namespace scoped resource. This may be set to "Cluster" (default) or "Namespace".

## IoK8SApiNetworkingV1IngressClassSpec
### Properties
* **controller**: string: Controller refers to the name of the controller that should handle this class. This allows for different "flavors" that are controlled by the same controller. For example, you may have different Parameters for the same implementing controller. This should be specified as a domain-prefixed path no more than 250 characters in length, e.g. "acme.io/ingress-controller". This field is immutable.
* **parameters**: [IoK8SApiNetworkingV1IngressClassParametersReference](#iok8sapinetworkingv1ingressclassparametersreference): IngressClassParametersReference identifies an API object. This can be used to specify a cluster or namespace-scoped resource.

## IoK8SApiNetworkingV1IngressRule
### Properties
* **host**: string: Host is the fully qualified domain name of a network host, as defined by RFC 3986. Note the following deviations from the "host" part of the URI as defined in RFC 3986: 1. IPs are not allowed. Currently an IngressRuleValue can only apply to
   the IP in the Spec of the parent Ingress.
2. The `:` delimiter is not respected because ports are not allowed.
	  Currently the port of an Ingress is implicitly :80 for http and
	  :443 for https.
Both these may change in the future. Incoming requests are matched against the host before the IngressRuleValue. If the host is unspecified, the Ingress routes all traffic based on the specified IngressRuleValue.

Host can be "precise" which is a domain name without the terminating dot of a network host (e.g. "foo.bar.com") or "wildcard", which is a domain name prefixed with a single wildcard label (e.g. "*.foo.com"). The wildcard character '*' must appear by itself as the first DNS label and matches only a single label. You cannot have a wildcard label by itself (e.g. Host == "*"). Requests will be matched against the Host field in the following way: 1. If Host is precise, the request matches this rule if the http host header is equal to Host. 2. If Host is a wildcard, then the request matches this rule if the http host header is to equal to the suffix (removing the first label) of the wildcard rule.
* **http**: [IoK8SApiNetworkingV1HttpIngressRuleValue](#iok8sapinetworkingv1httpingressrulevalue): HTTPIngressRuleValue is a list of http selectors pointing to backends. In the example: http://<host>/<path>?<searchpart> -> backend where where parts of the url correspond to RFC 3986, this resource will be used to match against everything after the last '/' and before the first '?' or '#'.

## IoK8SApiNetworkingV1IngressServiceBackend
### Properties
* **name**: string (Required): Name is the referenced service. The service must exist in the same namespace as the Ingress object.
* **port**: [IoK8SApiNetworkingV1ServiceBackendPort](#iok8sapinetworkingv1servicebackendport): ServiceBackendPort is the service port being referenced.

## IoK8SApiNetworkingV1IngressSpec
### Properties
* **defaultBackend**: [IoK8SApiNetworkingV1IngressBackend](#iok8sapinetworkingv1ingressbackend): IngressBackend describes all endpoints for a given service and port.
* **ingressClassName**: string: IngressClassName is the name of an IngressClass cluster resource. Ingress controller implementations use this field to know whether they should be serving this Ingress resource, by a transitive connection (controller -> IngressClass -> Ingress resource). Although the `kubernetes.io/ingress.class` annotation (simple constant name) was never formally defined, it was widely supported by Ingress controllers to create a direct binding between Ingress controller and Ingress resources. Newly created Ingress resources should prefer using the field. However, even though the annotation is officially deprecated, for backwards compatibility reasons, ingress controllers should still honor that annotation if present.
* **rules**: [IoK8SApiNetworkingV1IngressRule](#iok8sapinetworkingv1ingressrule)[]: A list of host rules used to configure the Ingress. If unspecified, or no rule matches, all traffic is sent to the default backend.
* **tls**: [IoK8SApiNetworkingV1IngressTLS](#iok8sapinetworkingv1ingresstls)[]: TLS configuration. Currently the Ingress only supports a single TLS port, 443. If multiple members of this list specify different hosts, they will be multiplexed on the same port according to the hostname specified through the SNI TLS extension, if the ingress controller fulfilling the ingress supports SNI.

## IoK8SApiNetworkingV1IngressStatus
### Properties
* **loadBalancer**: [IoK8SApiCoreV1LoadBalancerStatus](#iok8sapicorev1loadbalancerstatus): LoadBalancerStatus represents the status of a load-balancer.

## IoK8SApiNetworkingV1IngressTLS
### Properties
* **hosts**: string[]: Hosts are a list of hosts included in the TLS certificate. The values in this list must match the name/s used in the tlsSecret. Defaults to the wildcard host setting for the loadbalancer controller fulfilling this Ingress, if left unspecified.
* **secretName**: string: SecretName is the name of the secret used to terminate TLS traffic on port 443. Field is left optional to allow TLS routing based on SNI hostname alone. If the SNI host in a listener conflicts with the "Host" header field used by an IngressRule, the SNI host is used for termination and value of the Host header is used for routing.

## IoK8SApiNetworkingV1IPBlock
### Properties
* **cidr**: string (Required): CIDR is a string representing the IP Block Valid examples are "192.168.1.1/24" or "2001:db9::/64"
* **except**: string[]: Except is a slice of CIDRs that should not be included within an IP Block Valid examples are "192.168.1.1/24" or "2001:db9::/64" Except values will be rejected if they are outside the CIDR range

## IoK8SApiNetworkingV1NetworkPolicyEgressRule
### Properties
* **ports**: [IoK8SApiNetworkingV1NetworkPolicyPort](#iok8sapinetworkingv1networkpolicyport)[]: List of destination ports for outgoing traffic. Each item in this list is combined using a logical OR. If this field is empty or missing, this rule matches all ports (traffic not restricted by port). If this field is present and contains at least one item, then this rule allows traffic only if the traffic matches at least one port in the list.
* **to**: [IoK8SApiNetworkingV1NetworkPolicyPeer](#iok8sapinetworkingv1networkpolicypeer)[]: List of destinations for outgoing traffic of pods selected for this rule. Items in this list are combined using a logical OR operation. If this field is empty or missing, this rule matches all destinations (traffic not restricted by destination). If this field is present and contains at least one item, this rule allows traffic only if the traffic matches at least one item in the to list.

## IoK8SApiNetworkingV1NetworkPolicyIngressRule
### Properties
* **from**: [IoK8SApiNetworkingV1NetworkPolicyPeer](#iok8sapinetworkingv1networkpolicypeer)[]: List of sources which should be able to access the pods selected for this rule. Items in this list are combined using a logical OR operation. If this field is empty or missing, this rule matches all sources (traffic not restricted by source). If this field is present and contains at least one item, this rule allows traffic only if the traffic matches at least one item in the from list.
* **ports**: [IoK8SApiNetworkingV1NetworkPolicyPort](#iok8sapinetworkingv1networkpolicyport)[]: List of ports which should be made accessible on the pods selected for this rule. Each item in this list is combined using a logical OR. If this field is empty or missing, this rule matches all ports (traffic not restricted by port). If this field is present and contains at least one item, then this rule allows traffic only if the traffic matches at least one port in the list.

## IoK8SApiNetworkingV1NetworkPolicyPeer
### Properties
* **ipBlock**: [IoK8SApiNetworkingV1IPBlock](#iok8sapinetworkingv1ipblock): IPBlock describes a particular CIDR (Ex. "192.168.1.1/24","2001:db9::/64") that is allowed to the pods matched by a NetworkPolicySpec's podSelector. The except entry describes CIDRs that should not be included within this rule.
* **namespaceSelector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **podSelector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.

## IoK8SApiNetworkingV1NetworkPolicyPort
### Properties
* **endPort**: int: If set, indicates that the range of ports from port to endPort, inclusive, should be allowed by the policy. This field cannot be defined if the port field is not defined or if the port field is defined as a named (string) port. The endPort must be equal or greater than port.
* **port**: string: IntOrString is a type that can hold an int32 or a string.  When used in JSON or YAML marshalling and unmarshalling, it produces or consumes the inner type.  This allows you to have, for example, a JSON field that can accept a name or number.
* **protocol**: string: The protocol (TCP, UDP, or SCTP) which traffic must match. If not specified, this field defaults to TCP.

## IoK8SApiNetworkingV1NetworkPolicySpec
### Properties
* **egress**: [IoK8SApiNetworkingV1NetworkPolicyEgressRule](#iok8sapinetworkingv1networkpolicyegressrule)[]: List of egress rules to be applied to the selected pods. Outgoing traffic is allowed if there are no NetworkPolicies selecting the pod (and cluster policy otherwise allows the traffic), OR if the traffic matches at least one egress rule across all of the NetworkPolicy objects whose podSelector matches the pod. If this field is empty then this NetworkPolicy limits all outgoing traffic (and serves solely to ensure that the pods it selects are isolated by default). This field is beta-level in 1.8
* **ingress**: [IoK8SApiNetworkingV1NetworkPolicyIngressRule](#iok8sapinetworkingv1networkpolicyingressrule)[]: List of ingress rules to be applied to the selected pods. Traffic is allowed to a pod if there are no NetworkPolicies selecting the pod (and cluster policy otherwise allows the traffic), OR if the traffic source is the pod's local node, OR if the traffic matches at least one ingress rule across all of the NetworkPolicy objects whose podSelector matches the pod. If this field is empty then this NetworkPolicy does not allow any traffic (and serves solely to ensure that the pods it selects are isolated by default)
* **podSelector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector) (Required): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **policyTypes**: string[]: List of rule types that the NetworkPolicy relates to. Valid options are ["Ingress"], ["Egress"], or ["Ingress", "Egress"]. If this field is not specified, it will default based on the existence of Ingress or Egress rules; policies that contain an Egress section are assumed to affect Egress, and all policies (whether or not they contain an Ingress section) are assumed to affect Ingress. If you want to write an egress-only policy, you must explicitly specify policyTypes [ "Egress" ]. Likewise, if you want to write a policy that specifies that no egress is allowed, you must specify a policyTypes value that include "Egress" (since such a policy would not include an Egress section and would otherwise default to just [ "Ingress" ]). This field is beta-level in 1.8

## IoK8SApiNetworkingV1NetworkPolicyStatus
### Properties
* **conditions**: [IoK8SApimachineryPkgApisMetaV1Condition](#iok8sapimachinerypkgapismetav1condition)[]: Conditions holds an array of metav1.Condition that describe the state of the NetworkPolicy. Current service state

## IoK8SApiNetworkingV1ServiceBackendPort
### Properties
* **name**: string: Name is the name of the port on the Service. This is a mutually exclusive setting with "Number".
* **number**: int: Number is the numerical port number (e.g. 80) on the Service. This is a mutually exclusive setting with "Name".

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
* **namespace**: string (DeployTimeConstant): The namespace of the resource.

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

