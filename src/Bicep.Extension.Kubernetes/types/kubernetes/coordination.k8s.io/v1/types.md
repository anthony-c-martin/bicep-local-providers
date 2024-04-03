# coordination.k8s.io @ v1

## Resource coordination.k8s.io/Lease@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'coordination.k8s.io/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'Lease' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiCoordinationV1LeaseSpec](#iok8sapicoordinationv1leasespec): LeaseSpec is a specification of a Lease.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoordinationV1LeaseSpec
### Properties
* **acquireTime**: string: MicroTime is version of Time with microsecond level precision.
* **holderIdentity**: string: holderIdentity contains the identity of the holder of a current lease.
* **leaseDurationSeconds**: int: leaseDurationSeconds is a duration that candidates for a lease need to wait to force acquire it. This is measure against time of last observed RenewTime.
* **leaseTransitions**: int: leaseTransitions is the number of transitions of a lease between holders.
* **renewTime**: string: MicroTime is version of Time with microsecond level precision.

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

