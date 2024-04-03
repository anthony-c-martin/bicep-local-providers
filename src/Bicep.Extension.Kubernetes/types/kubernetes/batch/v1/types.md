# batch @ v1

## Resource batch/CronJob@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'batch/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'CronJob' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiBatchV1CronJobSpec](#iok8sapibatchv1cronjobspec): CronJobSpec describes how the job execution will look like and when it will actually run.
* **status**: [IoK8SApiBatchV1CronJobStatus](#iok8sapibatchv1cronjobstatus): CronJobStatus represents the current state of a cron job.

## Resource batch/Job@v1
* **Valid Scope(s)**: Unknown
### Properties
* **apiVersion**: 'batch/v1' (ReadOnly, DeployTimeConstant): The api version.
* **kind**: 'Job' (ReadOnly, DeployTimeConstant): The resource kind.
* **metadata**: [metadata](#metadata) (Required): The resource metadata.
* **spec**: [IoK8SApiBatchV1JobSpec](#iok8sapibatchv1jobspec): JobSpec describes how the job execution will look like.
* **status**: [IoK8SApiBatchV1JobStatus](#iok8sapibatchv1jobstatus): JobStatus represents the current state of a Job.

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## annotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiBatchV1CronJobSpec
### Properties
* **concurrencyPolicy**: string: Specifies how to treat concurrent executions of a Job. Valid values are: - "Allow" (default): allows CronJobs to run concurrently; - "Forbid": forbids concurrent runs, skipping next run if previous run hasn't finished yet; - "Replace": cancels currently running job and replaces it with a new one
* **failedJobsHistoryLimit**: int: The number of failed finished jobs to retain. Value must be non-negative integer. Defaults to 1.
* **jobTemplate**: [IoK8SApiBatchV1JobTemplateSpec](#iok8sapibatchv1jobtemplatespec) (Required): JobTemplateSpec describes the data a Job should have when created from a template
* **schedule**: string (Required): The schedule in Cron format, see https://en.wikipedia.org/wiki/Cron.
* **startingDeadlineSeconds**: int: Optional deadline in seconds for starting the job if it misses scheduled time for any reason.  Missed jobs executions will be counted as failed ones.
* **successfulJobsHistoryLimit**: int: The number of successful finished jobs to retain. Value must be non-negative integer. Defaults to 3.
* **suspend**: bool: This flag tells the controller to suspend subsequent executions, it does not apply to already started executions.  Defaults to false.
* **timeZone**: string: The time zone name for the given schedule, see https://en.wikipedia.org/wiki/List_of_tz_database_time_zones. If not specified, this will default to the time zone of the kube-controller-manager process. The set of valid time zone names and the time zone offset is loaded from the system-wide time zone database by the API server during CronJob validation and the controller manager during execution. If no system-wide time zone database can be found a bundled version of the database is used instead. If the time zone name becomes invalid during the lifetime of a CronJob or due to a change in host configuration, the controller will stop creating new new Jobs and will create a system event with the reason UnknownTimeZone. More information can be found in https://kubernetes.io/docs/concepts/workloads/controllers/cron-jobs/#time-zones This is beta field and must be enabled via the `CronJobTimeZone` feature gate.

## IoK8SApiBatchV1CronJobStatus
### Properties
* **active**: [IoK8SApiCoreV1ObjectReference](#iok8sapicorev1objectreference)[]: A list of pointers to currently running jobs.
* **lastScheduleTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **lastSuccessfulTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.

## IoK8SApiBatchV1JobCondition
### Properties
* **lastProbeTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **lastTransitionTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **message**: string: Human readable message indicating details about last transition.
* **reason**: string: (brief) reason for the condition's last transition.
* **status**: string (Required): Status of the condition, one of True, False, Unknown.
* **type**: string (Required): Type of job condition, Complete or Failed.

## IoK8SApiBatchV1JobSpec
### Properties
* **activeDeadlineSeconds**: int: Specifies the duration in seconds relative to the startTime that the job may be continuously active before the system tries to terminate it; value must be positive integer. If a Job is suspended (at creation or through an update), this timer will effectively be stopped and reset when the Job is resumed again.
* **backoffLimit**: int: Specifies the number of retries before marking this job failed. Defaults to 6
* **completionMode**: string: CompletionMode specifies how Pod completions are tracked. It can be `NonIndexed` (default) or `Indexed`.

`NonIndexed` means that the Job is considered complete when there have been .spec.completions successfully completed Pods. Each Pod completion is homologous to each other.

`Indexed` means that the Pods of a Job get an associated completion index from 0 to (.spec.completions - 1), available in the annotation batch.kubernetes.io/job-completion-index. The Job is considered complete when there is one successfully completed Pod for each index. When value is `Indexed`, .spec.completions must be specified and `.spec.parallelism` must be less than or equal to 10^5. In addition, The Pod name takes the form `$(job-name)-$(index)-$(random-string)`, the Pod hostname takes the form `$(job-name)-$(index)`.

More completion modes can be added in the future. If the Job controller observes a mode that it doesn't recognize, which is possible during upgrades due to version skew, the controller skips updates for the Job.
* **completions**: int: Specifies the desired number of successfully finished pods the job should be run with.  Setting to nil means that the success of any pod signals the success of all pods, and allows parallelism to have any positive value.  Setting to 1 means that parallelism is limited to 1 and the success of that pod signals the success of the job. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
* **manualSelector**: bool: manualSelector controls generation of pod labels and pod selectors. Leave `manualSelector` unset unless you are certain what you are doing. When false or unset, the system pick labels unique to this job and appends those labels to the pod template.  When true, the user is responsible for picking unique labels and specifying the selector.  Failure to pick a unique label may cause this and other jobs to not function correctly.  However, You may see `manualSelector=true` in jobs that were created with the old `extensions/v1beta1` API. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/#specifying-your-own-pod-selector
* **parallelism**: int: Specifies the maximum desired number of pods the job should run at any given time. The actual number of pods running in steady state will be less than this number when ((.spec.completions - .status.successful) < .spec.parallelism), i.e. when the work left to do is less than max parallelism. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
* **podFailurePolicy**: [IoK8SApiBatchV1PodFailurePolicy](#iok8sapibatchv1podfailurepolicy): PodFailurePolicy describes how failed pods influence the backoffLimit.
* **selector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **suspend**: bool: Suspend specifies whether the Job controller should create Pods or not. If a Job is created with suspend set to true, no Pods are created by the Job controller. If a Job is suspended after creation (i.e. the flag goes from false to true), the Job controller will delete all active Pods associated with this Job. Users must design their workload to gracefully handle this. Suspending a Job will reset the StartTime field of the Job, effectively resetting the ActiveDeadlineSeconds timer too. Defaults to false.
* **template**: [IoK8SApiCoreV1PodTemplateSpec](#iok8sapicorev1podtemplatespec) (Required): PodTemplateSpec describes the data a pod should have when created from a template
* **ttlSecondsAfterFinished**: int: ttlSecondsAfterFinished limits the lifetime of a Job that has finished execution (either Complete or Failed). If this field is set, ttlSecondsAfterFinished after the Job finishes, it is eligible to be automatically deleted. When the Job is being deleted, its lifecycle guarantees (e.g. finalizers) will be honored. If this field is unset, the Job won't be automatically deleted. If this field is set to zero, the Job becomes eligible to be deleted immediately after it finishes.

## IoK8SApiBatchV1JobStatus
### Properties
* **active**: int: The number of pending and running pods.
* **completedIndexes**: string: CompletedIndexes holds the completed indexes when .spec.completionMode = "Indexed" in a text format. The indexes are represented as decimal integers separated by commas. The numbers are listed in increasing order. Three or more consecutive numbers are compressed and represented by the first and last element of the series, separated by a hyphen. For example, if the completed indexes are 1, 3, 4, 5 and 7, they are represented as "1,3-5,7".
* **completionTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **conditions**: [IoK8SApiBatchV1JobCondition](#iok8sapibatchv1jobcondition)[]: The latest available observations of an object's current state. When a Job fails, one of the conditions will have type "Failed" and status true. When a Job is suspended, one of the conditions will have type "Suspended" and status true; when the Job is resumed, the status of this condition will become false. When a Job is completed, one of the conditions will have type "Complete" and status true. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
* **failed**: int: The number of pods which reached phase Failed.
* **ready**: int: The number of pods which have a Ready condition.

This field is beta-level. The job controller populates the field when the feature gate JobReadyPods is enabled (enabled by default).
* **startTime**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **succeeded**: int: The number of pods which reached phase Succeeded.
* **uncountedTerminatedPods**: [IoK8SApiBatchV1UncountedTerminatedPods](#iok8sapibatchv1uncountedterminatedpods): UncountedTerminatedPods holds UIDs of Pods that have terminated but haven't been accounted in Job status counters.

## IoK8SApiBatchV1JobTemplateSpec
### Properties
* **metadata**: [IoK8SApimachineryPkgApisMetaV1ObjectMeta](#iok8sapimachinerypkgapismetav1objectmeta): ObjectMeta is metadata that all persisted resources must have, which includes all objects users must create.
* **spec**: [IoK8SApiBatchV1JobSpec](#iok8sapibatchv1jobspec): JobSpec describes how the job execution will look like.

## IoK8SApiBatchV1PodFailurePolicy
### Properties
* **rules**: [IoK8SApiBatchV1PodFailurePolicyRule](#iok8sapibatchv1podfailurepolicyrule)[] (Required): A list of pod failure policy rules. The rules are evaluated in order. Once a rule matches a Pod failure, the remaining of the rules are ignored. When no rule matches the Pod failure, the default handling applies - the counter of pod failures is incremented and it is checked against the backoffLimit. At most 20 elements are allowed.

## IoK8SApiBatchV1PodFailurePolicyOnExitCodesRequirement
### Properties
* **containerName**: string: Restricts the check for exit codes to the container with the specified name. When null, the rule applies to all containers. When specified, it should match one the container or initContainer names in the pod template.
* **operator**: string (Required): Represents the relationship between the container exit code(s) and the specified values. Containers completed with success (exit code 0) are excluded from the requirement check. Possible values are: - In: the requirement is satisfied if at least one container exit code
  (might be multiple if there are multiple containers not restricted
  by the 'containerName' field) is in the set of specified values.
- NotIn: the requirement is satisfied if at least one container exit code
  (might be multiple if there are multiple containers not restricted
  by the 'containerName' field) is not in the set of specified values.
Additional values are considered to be added in the future. Clients should react to an unknown operator by assuming the requirement is not satisfied.
* **values**: int[] (Required): Specifies the set of values. Each returned container exit code (might be multiple in case of multiple containers) is checked against this set of values with respect to the operator. The list of values must be ordered and must not contain duplicates. Value '0' cannot be used for the In operator. At least one element is required. At most 255 elements are allowed.

## IoK8SApiBatchV1PodFailurePolicyOnPodConditionsPattern
### Properties
* **status**: string (Required): Specifies the required Pod condition status. To match a pod condition it is required that the specified status equals the pod condition status. Defaults to True.
* **type**: string (Required): Specifies the required Pod condition type. To match a pod condition it is required that specified type equals the pod condition type.

## IoK8SApiBatchV1PodFailurePolicyRule
### Properties
* **action**: string (Required): Specifies the action taken on a pod failure when the requirements are satisfied. Possible values are: - FailJob: indicates that the pod's job is marked as Failed and all
  running pods are terminated.
- Ignore: indicates that the counter towards the .backoffLimit is not
  incremented and a replacement pod is created.
- Count: indicates that the pod is handled in the default way - the
  counter towards the .backoffLimit is incremented.
Additional values are considered to be added in the future. Clients should react to an unknown action by skipping the rule.
* **onExitCodes**: [IoK8SApiBatchV1PodFailurePolicyOnExitCodesRequirement](#iok8sapibatchv1podfailurepolicyonexitcodesrequirement): PodFailurePolicyOnExitCodesRequirement describes the requirement for handling a failed pod based on its container exit codes. In particular, it lookups the .state.terminated.exitCode for each app container and init container status, represented by the .status.containerStatuses and .status.initContainerStatuses fields in the Pod status, respectively. Containers completed with success (exit code 0) are excluded from the requirement check.
* **onPodConditions**: [IoK8SApiBatchV1PodFailurePolicyOnPodConditionsPattern](#iok8sapibatchv1podfailurepolicyonpodconditionspattern)[] (Required): Represents the requirement on the pod conditions. The requirement is represented as a list of pod condition patterns. The requirement is satisfied if at least one pattern matches an actual pod condition. At most 20 elements are allowed.

## IoK8SApiBatchV1UncountedTerminatedPods
### Properties
* **failed**: string[]: Failed holds UIDs of failed Pods.
* **succeeded**: string[]: Succeeded holds UIDs of succeeded Pods.

## IoK8SApiCoreV1Affinity
### Properties
* **nodeAffinity**: [IoK8SApiCoreV1NodeAffinity](#iok8sapicorev1nodeaffinity): Node affinity is a group of node affinity scheduling rules.
* **podAffinity**: [IoK8SApiCoreV1PodAffinity](#iok8sapicorev1podaffinity): Pod affinity is a group of inter pod affinity scheduling rules.
* **podAntiAffinity**: [IoK8SApiCoreV1PodAntiAffinity](#iok8sapicorev1podantiaffinity): Pod anti affinity is a group of inter pod anti affinity scheduling rules.

## IoK8SApiCoreV1AWSElasticBlockStoreVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
* **partition**: int: partition is the partition in the volume that you want to mount. If omitted, the default is to mount by volume name. Examples: For volume /dev/sda1, you specify the partition as "1". Similarly, the volume partition for /dev/sda is "0" (or you can leave the property empty).
* **readOnly**: bool: readOnly value true will force the readOnly setting in VolumeMounts. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
* **volumeID**: string (Required): volumeID is unique ID of the persistent disk resource in AWS (Amazon EBS volume). More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore

## IoK8SApiCoreV1AzureDiskVolumeSource
### Properties
* **cachingMode**: string: cachingMode is the Host Caching mode: None, Read Only, Read Write.
* **diskName**: string (Required): diskName is the Name of the data disk in the blob storage
* **diskURI**: string (Required): diskURI is the URI of data disk in the blob storage
* **fsType**: string: fsType is Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
* **kind**: string: kind expected values are Shared: multiple blob disks per storage account  Dedicated: single blob disk per storage account  Managed: azure managed data disk (only in managed availability set). defaults to shared
* **readOnly**: bool: readOnly Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.

## IoK8SApiCoreV1AzureFileVolumeSource
### Properties
* **readOnly**: bool: readOnly defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
* **secretName**: string (Required): secretName is the  name of secret that contains Azure Storage Account Name and Key
* **shareName**: string (Required): shareName is the azure share Name

## IoK8SApiCoreV1Capabilities
### Properties
* **add**: string[]: Added capabilities
* **drop**: string[]: Removed capabilities

## IoK8SApiCoreV1CephFSVolumeSource
### Properties
* **monitors**: string[] (Required): monitors is Required: Monitors is a collection of Ceph monitors More info: https://examples.k8s.io/volumes/cephfs/README.md#how-to-use-it
* **path**: string: path is Optional: Used as the mounted root, rather than the full Ceph tree, default is /
* **readOnly**: bool: readOnly is Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts. More info: https://examples.k8s.io/volumes/cephfs/README.md#how-to-use-it
* **secretFile**: string: secretFile is Optional: SecretFile is the path to key ring for User, default is /etc/ceph/user.secret More info: https://examples.k8s.io/volumes/cephfs/README.md#how-to-use-it
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **user**: string: user is optional: User is the rados user name, default is admin More info: https://examples.k8s.io/volumes/cephfs/README.md#how-to-use-it

## IoK8SApiCoreV1CinderVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://examples.k8s.io/mysql-cinder-pd/README.md
* **readOnly**: bool: readOnly defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts. More info: https://examples.k8s.io/mysql-cinder-pd/README.md
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **volumeID**: string (Required): volumeID used to identify the volume in cinder. More info: https://examples.k8s.io/mysql-cinder-pd/README.md

## IoK8SApiCoreV1ConfigMapEnvSource
### Properties
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: Specify whether the ConfigMap must be defined

## IoK8SApiCoreV1ConfigMapKeySelector
### Properties
* **key**: string (Required): The key to select.
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: Specify whether the ConfigMap or its key must be defined

## IoK8SApiCoreV1ConfigMapProjection
### Properties
* **items**: [IoK8SApiCoreV1KeyToPath](#iok8sapicorev1keytopath)[]: items if unspecified, each key-value pair in the Data field of the referenced ConfigMap will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the ConfigMap, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: optional specify whether the ConfigMap or its keys must be defined

## IoK8SApiCoreV1ConfigMapVolumeSource
### Properties
* **defaultMode**: int: defaultMode is optional: mode bits used to set permissions on created files by default. Must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
* **items**: [IoK8SApiCoreV1KeyToPath](#iok8sapicorev1keytopath)[]: items if unspecified, each key-value pair in the Data field of the referenced ConfigMap will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the ConfigMap, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: optional specify whether the ConfigMap or its keys must be defined

## IoK8SApiCoreV1Container
### Properties
* **args**: string[]: Arguments to the entrypoint. The container image's CMD is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
* **command**: string[]: Entrypoint array. Not executed within a shell. The container image's ENTRYPOINT is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
* **env**: [IoK8SApiCoreV1EnvVar](#iok8sapicorev1envvar)[]: List of environment variables to set in the container. Cannot be updated.
* **envFrom**: [IoK8SApiCoreV1EnvFromSource](#iok8sapicorev1envfromsource)[]: List of sources to populate environment variables in the container. The keys defined within a source must be a C_IDENTIFIER. All invalid keys will be reported as an event when the container is starting. When a key exists in multiple sources, the value associated with the last source will take precedence. Values defined by an Env with a duplicate key will take precedence. Cannot be updated.
* **image**: string: Container image name. More info: https://kubernetes.io/docs/concepts/containers/images This field is optional to allow higher level config management to default or override container images in workload controllers like Deployments and StatefulSets.
* **imagePullPolicy**: string: Image pull policy. One of Always, Never, IfNotPresent. Defaults to Always if :latest tag is specified, or IfNotPresent otherwise. Cannot be updated. More info: https://kubernetes.io/docs/concepts/containers/images#updating-images
* **lifecycle**: [IoK8SApiCoreV1Lifecycle](#iok8sapicorev1lifecycle): Lifecycle describes actions that the management system should take in response to container lifecycle events. For the PostStart and PreStop lifecycle handlers, management of the container blocks until the action is complete, unless the container process fails, in which case the handler is aborted.
* **livenessProbe**: [IoK8SApiCoreV1Probe](#iok8sapicorev1probe): Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
* **name**: string (Required): Name of the container specified as a DNS_LABEL. Each container in a pod must have a unique name (DNS_LABEL). Cannot be updated.
* **ports**: [IoK8SApiCoreV1ContainerPort](#iok8sapicorev1containerport)[]: List of ports to expose from the container. Not specifying a port here DOES NOT prevent that port from being exposed. Any port which is listening on the default "0.0.0.0" address inside a container will be accessible from the network. Modifying this array with strategic merge patch may corrupt the data. For more information See https://github.com/kubernetes/kubernetes/issues/108255. Cannot be updated.
* **readinessProbe**: [IoK8SApiCoreV1Probe](#iok8sapicorev1probe): Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
* **resources**: [IoK8SApiCoreV1ResourceRequirements](#iok8sapicorev1resourcerequirements): ResourceRequirements describes the compute resource requirements.
* **securityContext**: [IoK8SApiCoreV1SecurityContext](#iok8sapicorev1securitycontext): SecurityContext holds security configuration that will be applied to a container. Some fields are present in both SecurityContext and PodSecurityContext.  When both are set, the values in SecurityContext take precedence.
* **startupProbe**: [IoK8SApiCoreV1Probe](#iok8sapicorev1probe): Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
* **stdin**: bool: Whether this container should allocate a buffer for stdin in the container runtime. If this is not set, reads from stdin in the container will always result in EOF. Default is false.
* **stdinOnce**: bool: Whether the container runtime should close the stdin channel after it has been opened by a single attach. When stdin is true the stdin stream will remain open across multiple attach sessions. If stdinOnce is set to true, stdin is opened on container start, is empty until the first client attaches to stdin, and then remains open and accepts data until the client disconnects, at which time stdin is closed and remains closed until the container is restarted. If this flag is false, a container processes that reads from stdin will never receive an EOF. Default is false
* **terminationMessagePath**: string: Optional: Path at which the file to which the container's termination message will be written is mounted into the container's filesystem. Message written is intended to be brief final status, such as an assertion failure message. Will be truncated by the node if greater than 4096 bytes. The total message length across all containers will be limited to 12kb. Defaults to /dev/termination-log. Cannot be updated.
* **terminationMessagePolicy**: string: Indicate how the termination message should be populated. File will use the contents of terminationMessagePath to populate the container status message on both success and failure. FallbackToLogsOnError will use the last chunk of container log output if the termination message file is empty and the container exited with an error. The log output is limited to 2048 bytes or 80 lines, whichever is smaller. Defaults to File. Cannot be updated.
* **tty**: bool: Whether this container should allocate a TTY for itself, also requires 'stdin' to be true. Default is false.
* **volumeDevices**: [IoK8SApiCoreV1VolumeDevice](#iok8sapicorev1volumedevice)[]: volumeDevices is the list of block devices to be used by the container.
* **volumeMounts**: [IoK8SApiCoreV1VolumeMount](#iok8sapicorev1volumemount)[]: Pod volumes to mount into the container's filesystem. Cannot be updated.
* **workingDir**: string: Container's working directory. If not specified, the container runtime's default will be used, which might be configured in the container image. Cannot be updated.

## IoK8SApiCoreV1ContainerPort
### Properties
* **containerPort**: int (Required): Number of port to expose on the pod's IP address. This must be a valid port number, 0 < x < 65536.
* **hostIP**: string: What host IP to bind the external port to.
* **hostPort**: int: Number of port to expose on the host. If specified, this must be a valid port number, 0 < x < 65536. If HostNetwork is specified, this must match ContainerPort. Most containers do not need this.
* **name**: string: If specified, this must be an IANA_SVC_NAME and unique within the pod. Each named port in a pod must have a unique name. Name for the port that can be referred to by services.
* **protocol**: string: Protocol for port. Must be UDP, TCP, or SCTP. Defaults to "TCP".

## IoK8SApiCoreV1CSIVolumeSource
### Properties
* **driver**: string (Required): driver is the name of the CSI driver that handles this volume. Consult with your admin for the correct name as registered in the cluster.
* **fsType**: string: fsType to mount. Ex. "ext4", "xfs", "ntfs". If not provided, the empty value is passed to the associated CSI driver which will determine the default filesystem to apply.
* **nodePublishSecretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **readOnly**: bool: readOnly specifies a read-only configuration for the volume. Defaults to false (read/write).
* **volumeAttributes**: [IoK8SApiCoreV1CSIVolumeSourceVolumeAttributes](#iok8sapicorev1csivolumesourcevolumeattributes): volumeAttributes stores driver-specific properties that are passed to the CSI driver. Consult your driver's documentation for supported values.

## IoK8SApiCoreV1CSIVolumeSourceVolumeAttributes
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1DownwardAPIProjection
### Properties
* **items**: [IoK8SApiCoreV1DownwardAPIVolumeFile](#iok8sapicorev1downwardapivolumefile)[]: Items is a list of DownwardAPIVolume file

## IoK8SApiCoreV1DownwardAPIVolumeFile
### Properties
* **fieldRef**: [IoK8SApiCoreV1ObjectFieldSelector](#iok8sapicorev1objectfieldselector): ObjectFieldSelector selects an APIVersioned field of an object.
* **mode**: int: Optional: mode bits used to set permissions on this file, must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. If not specified, the volume defaultMode will be used. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
* **path**: string (Required): Required: Path is  the relative path name of the file to be created. Must not be absolute or contain the '..' path. Must be utf-8 encoded. The first item of the relative path must not start with '..'
* **resourceFieldRef**: [IoK8SApiCoreV1ResourceFieldSelector](#iok8sapicorev1resourcefieldselector): ResourceFieldSelector represents container resources (cpu, memory) and their output format

## IoK8SApiCoreV1DownwardAPIVolumeSource
### Properties
* **defaultMode**: int: Optional: mode bits to use on created files by default. Must be a Optional: mode bits used to set permissions on created files by default. Must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
* **items**: [IoK8SApiCoreV1DownwardAPIVolumeFile](#iok8sapicorev1downwardapivolumefile)[]: Items is a list of downward API volume file

## IoK8SApiCoreV1EmptyDirVolumeSource
### Properties
* **medium**: string: medium represents what type of storage medium should back this directory. The default is "" which means to use the node's default medium. Must be an empty string (default) or Memory. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
* **sizeLimit**: string: Quantity is a fixed-point representation of a number. It provides convenient marshaling/unmarshaling in JSON and YAML, in addition to String() and AsInt64() accessors.

The serialization format is:

``` <quantity>        ::= <signedNumber><suffix>

	(Note that <suffix> may be empty, from the "" case in <decimalSI>.)

<digit>           ::= 0 | 1 | ... | 9 <digits>          ::= <digit> | <digit><digits> <number>          ::= <digits> | <digits>.<digits> | <digits>. | .<digits> <sign>            ::= "+" | "-" <signedNumber>    ::= <number> | <sign><number> <suffix>          ::= <binarySI> | <decimalExponent> | <decimalSI> <binarySI>        ::= Ki | Mi | Gi | Ti | Pi | Ei

	(International System of units; See: http://physics.nist.gov/cuu/Units/binary.html)

<decimalSI>       ::= m | "" | k | M | G | T | P | E

	(Note that 1024 = 1Ki but 1000 = 1k; I didn't choose the capitalization.)

<decimalExponent> ::= "e" <signedNumber> | "E" <signedNumber> ```

No matter which of the three exponent forms is used, no quantity may represent a number greater than 2^63-1 in magnitude, nor may it have more than 3 decimal places. Numbers larger or more precise will be capped or rounded up. (E.g.: 0.1m will rounded up to 1m.) This may be extended in the future if we require larger or smaller quantities.

When a Quantity is parsed from a string, it will remember the type of suffix it had, and will use the same type again when it is serialized.

Before serializing, Quantity will be put in "canonical form". This means that Exponent/suffix will be adjusted up or down (with a corresponding increase or decrease in Mantissa) such that:

- No precision is lost - No fractional digits will be emitted - The exponent (or suffix) is as large as possible.

The sign will be omitted unless the number is negative.

Examples:

- 1.5 will be serialized as "1500m" - 1.5Gi will be serialized as "1536Mi"

Note that the quantity will NEVER be internally represented by a floating point number. That is the whole point of this exercise.

Non-canonical values will still parse as long as they are well formed, but will be re-emitted in their canonical form. (So always use canonical form, or don't diff.)

This format is intended to make it difficult to use these numbers without writing some sort of special handling code in the hopes that that will cause implementors to also use a fixed point implementation.

## IoK8SApiCoreV1EnvFromSource
### Properties
* **configMapRef**: [IoK8SApiCoreV1ConfigMapEnvSource](#iok8sapicorev1configmapenvsource): ConfigMapEnvSource selects a ConfigMap to populate the environment variables with.

The contents of the target ConfigMap's Data field will represent the key-value pairs as environment variables.
* **prefix**: string: An optional identifier to prepend to each key in the ConfigMap. Must be a C_IDENTIFIER.
* **secretRef**: [IoK8SApiCoreV1SecretEnvSource](#iok8sapicorev1secretenvsource): SecretEnvSource selects a Secret to populate the environment variables with.

The contents of the target Secret's Data field will represent the key-value pairs as environment variables.

## IoK8SApiCoreV1EnvVar
### Properties
* **name**: string (Required): Name of the environment variable. Must be a C_IDENTIFIER.
* **value**: string: Variable references $(VAR_NAME) are expanded using the previously defined environment variables in the container and any service environment variables. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Defaults to "".
* **valueFrom**: [IoK8SApiCoreV1EnvVarSource](#iok8sapicorev1envvarsource): EnvVarSource represents a source for the value of an EnvVar.

## IoK8SApiCoreV1EnvVarSource
### Properties
* **configMapKeyRef**: [IoK8SApiCoreV1ConfigMapKeySelector](#iok8sapicorev1configmapkeyselector): Selects a key from a ConfigMap.
* **fieldRef**: [IoK8SApiCoreV1ObjectFieldSelector](#iok8sapicorev1objectfieldselector): ObjectFieldSelector selects an APIVersioned field of an object.
* **resourceFieldRef**: [IoK8SApiCoreV1ResourceFieldSelector](#iok8sapicorev1resourcefieldselector): ResourceFieldSelector represents container resources (cpu, memory) and their output format
* **secretKeyRef**: [IoK8SApiCoreV1SecretKeySelector](#iok8sapicorev1secretkeyselector): SecretKeySelector selects a key of a Secret.

## IoK8SApiCoreV1EphemeralContainer
### Properties
* **args**: string[]: Arguments to the entrypoint. The image's CMD is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
* **command**: string[]: Entrypoint array. Not executed within a shell. The image's ENTRYPOINT is used if this is not provided. Variable references $(VAR_NAME) are expanded using the container's environment. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Cannot be updated. More info: https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell
* **env**: [IoK8SApiCoreV1EnvVar](#iok8sapicorev1envvar)[]: List of environment variables to set in the container. Cannot be updated.
* **envFrom**: [IoK8SApiCoreV1EnvFromSource](#iok8sapicorev1envfromsource)[]: List of sources to populate environment variables in the container. The keys defined within a source must be a C_IDENTIFIER. All invalid keys will be reported as an event when the container is starting. When a key exists in multiple sources, the value associated with the last source will take precedence. Values defined by an Env with a duplicate key will take precedence. Cannot be updated.
* **image**: string: Container image name. More info: https://kubernetes.io/docs/concepts/containers/images
* **imagePullPolicy**: string: Image pull policy. One of Always, Never, IfNotPresent. Defaults to Always if :latest tag is specified, or IfNotPresent otherwise. Cannot be updated. More info: https://kubernetes.io/docs/concepts/containers/images#updating-images
* **lifecycle**: [IoK8SApiCoreV1Lifecycle](#iok8sapicorev1lifecycle): Lifecycle describes actions that the management system should take in response to container lifecycle events. For the PostStart and PreStop lifecycle handlers, management of the container blocks until the action is complete, unless the container process fails, in which case the handler is aborted.
* **livenessProbe**: [IoK8SApiCoreV1Probe](#iok8sapicorev1probe): Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
* **name**: string (Required): Name of the ephemeral container specified as a DNS_LABEL. This name must be unique among all containers, init containers and ephemeral containers.
* **ports**: [IoK8SApiCoreV1ContainerPort](#iok8sapicorev1containerport)[]: Ports are not allowed for ephemeral containers.
* **readinessProbe**: [IoK8SApiCoreV1Probe](#iok8sapicorev1probe): Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
* **resources**: [IoK8SApiCoreV1ResourceRequirements](#iok8sapicorev1resourcerequirements): ResourceRequirements describes the compute resource requirements.
* **securityContext**: [IoK8SApiCoreV1SecurityContext](#iok8sapicorev1securitycontext): SecurityContext holds security configuration that will be applied to a container. Some fields are present in both SecurityContext and PodSecurityContext.  When both are set, the values in SecurityContext take precedence.
* **startupProbe**: [IoK8SApiCoreV1Probe](#iok8sapicorev1probe): Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
* **stdin**: bool: Whether this container should allocate a buffer for stdin in the container runtime. If this is not set, reads from stdin in the container will always result in EOF. Default is false.
* **stdinOnce**: bool: Whether the container runtime should close the stdin channel after it has been opened by a single attach. When stdin is true the stdin stream will remain open across multiple attach sessions. If stdinOnce is set to true, stdin is opened on container start, is empty until the first client attaches to stdin, and then remains open and accepts data until the client disconnects, at which time stdin is closed and remains closed until the container is restarted. If this flag is false, a container processes that reads from stdin will never receive an EOF. Default is false
* **targetContainerName**: string: If set, the name of the container from PodSpec that this ephemeral container targets. The ephemeral container will be run in the namespaces (IPC, PID, etc) of this container. If not set then the ephemeral container uses the namespaces configured in the Pod spec.

The container runtime must implement support for this feature. If the runtime does not support namespace targeting then the result of setting this field is undefined.
* **terminationMessagePath**: string: Optional: Path at which the file to which the container's termination message will be written is mounted into the container's filesystem. Message written is intended to be brief final status, such as an assertion failure message. Will be truncated by the node if greater than 4096 bytes. The total message length across all containers will be limited to 12kb. Defaults to /dev/termination-log. Cannot be updated.
* **terminationMessagePolicy**: string: Indicate how the termination message should be populated. File will use the contents of terminationMessagePath to populate the container status message on both success and failure. FallbackToLogsOnError will use the last chunk of container log output if the termination message file is empty and the container exited with an error. The log output is limited to 2048 bytes or 80 lines, whichever is smaller. Defaults to File. Cannot be updated.
* **tty**: bool: Whether this container should allocate a TTY for itself, also requires 'stdin' to be true. Default is false.
* **volumeDevices**: [IoK8SApiCoreV1VolumeDevice](#iok8sapicorev1volumedevice)[]: volumeDevices is the list of block devices to be used by the container.
* **volumeMounts**: [IoK8SApiCoreV1VolumeMount](#iok8sapicorev1volumemount)[]: Pod volumes to mount into the container's filesystem. Subpath mounts are not allowed for ephemeral containers. Cannot be updated.
* **workingDir**: string: Container's working directory. If not specified, the container runtime's default will be used, which might be configured in the container image. Cannot be updated.

## IoK8SApiCoreV1EphemeralVolumeSource
### Properties
* **volumeClaimTemplate**: [IoK8SApiCoreV1PersistentVolumeClaimTemplate](#iok8sapicorev1persistentvolumeclaimtemplate): PersistentVolumeClaimTemplate is used to produce PersistentVolumeClaim objects as part of an EphemeralVolumeSource.

## IoK8SApiCoreV1ExecAction
### Properties
* **command**: string[]: Command is the command line to execute inside the container, the working directory for the command  is root ('/') in the container's filesystem. The command is simply exec'd, it is not run inside a shell, so traditional shell instructions ('|', etc) won't work. To use a shell, you need to explicitly call out to that shell. Exit status of 0 is treated as live/healthy and non-zero is unhealthy.

## IoK8SApiCoreV1FCVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
* **lun**: int: lun is Optional: FC target lun number
* **readOnly**: bool: readOnly is Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
* **targetWWNs**: string[]: targetWWNs is Optional: FC target worldwide names (WWNs)
* **wwids**: string[]: wwids Optional: FC volume world wide identifiers (wwids) Either wwids or combination of targetWWNs and lun must be set, but not both simultaneously.

## IoK8SApiCoreV1FlexVolumeSource
### Properties
* **driver**: string (Required): driver is the name of the driver to use for this volume.
* **fsType**: string: fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". The default filesystem depends on FlexVolume script.
* **options**: [IoK8SApiCoreV1FlexVolumeSourceOptions](#iok8sapicorev1flexvolumesourceoptions): options is Optional: this field holds extra command options if any.
* **readOnly**: bool: readOnly is Optional: defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.

## IoK8SApiCoreV1FlexVolumeSourceOptions
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1FlockerVolumeSource
### Properties
* **datasetName**: string: datasetName is Name of the dataset stored as metadata -> name on the dataset for Flocker should be considered as deprecated
* **datasetUUID**: string: datasetUUID is the UUID of the dataset. This is unique identifier of a Flocker dataset

## IoK8SApiCoreV1GCEPersistentDiskVolumeSource
### Properties
* **fsType**: string: fsType is filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
* **partition**: int: partition is the partition in the volume that you want to mount. If omitted, the default is to mount by volume name. Examples: For volume /dev/sda1, you specify the partition as "1". Similarly, the volume partition for /dev/sda is "0" (or you can leave the property empty). More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
* **pdName**: string (Required): pdName is unique name of the PD resource in GCE. Used to identify the disk in GCE. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
* **readOnly**: bool: readOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk

## IoK8SApiCoreV1GitRepoVolumeSource
### Properties
* **directory**: string: directory is the target directory name. Must not contain or start with '..'.  If '.' is supplied, the volume directory will be the git repository.  Otherwise, if specified, the volume will contain the git repository in the subdirectory with the given name.
* **repository**: string (Required): repository is the URL
* **revision**: string: revision is the commit hash for the specified revision.

## IoK8SApiCoreV1GlusterfsVolumeSource
### Properties
* **endpoints**: string (Required): endpoints is the endpoint name that details Glusterfs topology. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod
* **path**: string (Required): path is the Glusterfs volume path. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod
* **readOnly**: bool: readOnly here will force the Glusterfs volume to be mounted with read-only permissions. Defaults to false. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod

## IoK8SApiCoreV1GrpcAction
### Properties
* **port**: int (Required): Port number of the gRPC service. Number must be in the range 1 to 65535.
* **service**: string: Service is the name of the service to place in the gRPC HealthCheckRequest (see https://github.com/grpc/grpc/blob/master/doc/health-checking.md).

If this is not specified, the default behavior is defined by gRPC.

## IoK8SApiCoreV1HostAlias
### Properties
* **hostnames**: string[]: Hostnames for the above IP address.
* **ip**: string: IP address of the host file entry.

## IoK8SApiCoreV1HostPathVolumeSource
### Properties
* **path**: string (Required): path of the directory on the host. If the path is a symlink, it will follow the link to the real path. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
* **type**: string: type for HostPath Volume Defaults to "" More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath

## IoK8SApiCoreV1HttpGetAction
### Properties
* **host**: string: Host name to connect to, defaults to the pod IP. You probably want to set "Host" in httpHeaders instead.
* **httpHeaders**: [IoK8SApiCoreV1HttpHeader](#iok8sapicorev1httpheader)[]: Custom headers to set in the request. HTTP allows repeated headers.
* **path**: string: Path to access on the HTTP server.
* **port**: string (Required): IntOrString is a type that can hold an int32 or a string.  When used in JSON or YAML marshalling and unmarshalling, it produces or consumes the inner type.  This allows you to have, for example, a JSON field that can accept a name or number.
* **scheme**: string: Scheme to use for connecting to the host. Defaults to HTTP.

## IoK8SApiCoreV1HttpHeader
### Properties
* **name**: string (Required): The header field name
* **value**: string (Required): The header field value

## IoK8SApiCoreV1IscsiVolumeSource
### Properties
* **chapAuthDiscovery**: bool: chapAuthDiscovery defines whether support iSCSI Discovery CHAP authentication
* **chapAuthSession**: bool: chapAuthSession defines whether support iSCSI Session CHAP authentication
* **fsType**: string: fsType is the filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#iscsi
* **initiatorName**: string: initiatorName is the custom iSCSI Initiator Name. If initiatorName is specified with iscsiInterface simultaneously, new iSCSI interface <target portal>:<volume name> will be created for the connection.
* **iqn**: string (Required): iqn is the target iSCSI Qualified Name.
* **iscsiInterface**: string: iscsiInterface is the interface Name that uses an iSCSI transport. Defaults to 'default' (tcp).
* **lun**: int (Required): lun represents iSCSI Target Lun number.
* **portals**: string[]: portals is the iSCSI Target Portal List. The portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
* **readOnly**: bool: readOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false.
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **targetPortal**: string (Required): targetPortal is iSCSI Target Portal. The Portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).

## IoK8SApiCoreV1KeyToPath
### Properties
* **key**: string (Required): key is the key to project.
* **mode**: int: mode is Optional: mode bits used to set permissions on this file. Must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. If not specified, the volume defaultMode will be used. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
* **path**: string (Required): path is the relative path of the file to map the key to. May not be an absolute path. May not contain the path element '..'. May not start with the string '..'.

## IoK8SApiCoreV1Lifecycle
### Properties
* **postStart**: [IoK8SApiCoreV1LifecycleHandler](#iok8sapicorev1lifecyclehandler): LifecycleHandler defines a specific action that should be taken in a lifecycle hook. One and only one of the fields, except TCPSocket must be specified.
* **preStop**: [IoK8SApiCoreV1LifecycleHandler](#iok8sapicorev1lifecyclehandler): LifecycleHandler defines a specific action that should be taken in a lifecycle hook. One and only one of the fields, except TCPSocket must be specified.

## IoK8SApiCoreV1LifecycleHandler
### Properties
* **exec**: [IoK8SApiCoreV1ExecAction](#iok8sapicorev1execaction): ExecAction describes a "run in container" action.
* **httpGet**: [IoK8SApiCoreV1HttpGetAction](#iok8sapicorev1httpgetaction): HTTPGetAction describes an action based on HTTP Get requests.
* **tcpSocket**: [IoK8SApiCoreV1TCPSocketAction](#iok8sapicorev1tcpsocketaction): TCPSocketAction describes an action based on opening a socket

## IoK8SApiCoreV1LocalObjectReference
### Properties
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names

## IoK8SApiCoreV1NFSVolumeSource
### Properties
* **path**: string (Required): path that is exported by the NFS server. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
* **readOnly**: bool: readOnly here will force the NFS export to be mounted with read-only permissions. Defaults to false. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
* **server**: string (Required): server is the hostname or IP address of the NFS server. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs

## IoK8SApiCoreV1NodeAffinity
### Properties
* **preferredDuringSchedulingIgnoredDuringExecution**: [IoK8SApiCoreV1PreferredSchedulingTerm](#iok8sapicorev1preferredschedulingterm)[]: The scheduler will prefer to schedule pods to nodes that satisfy the affinity expressions specified by this field, but it may choose a node that violates one or more of the expressions. The node that is most preferred is the one with the greatest sum of weights, i.e. for each node that meets all of the scheduling requirements (resource request, requiredDuringScheduling affinity expressions, etc.), compute a sum by iterating through the elements of this field and adding "weight" to the sum if the node matches the corresponding matchExpressions; the node(s) with the highest sum are the most preferred.
* **requiredDuringSchedulingIgnoredDuringExecution**: [IoK8SApiCoreV1NodeSelector](#iok8sapicorev1nodeselector): A node selector represents the union of the results of one or more label queries over a set of nodes; that is, it represents the OR of the selectors represented by the node selector terms.

## IoK8SApiCoreV1NodeSelector
### Properties
* **nodeSelectorTerms**: [IoK8SApiCoreV1NodeSelectorTerm](#iok8sapicorev1nodeselectorterm)[] (Required): Required. A list of node selector terms. The terms are ORed.

## IoK8SApiCoreV1NodeSelectorRequirement
### Properties
* **key**: string (Required): The label key that the selector applies to.
* **operator**: string (Required): Represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists, DoesNotExist. Gt, and Lt.
* **values**: string[]: An array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. If the operator is Gt or Lt, the values array must have a single element, which will be interpreted as an integer. This array is replaced during a strategic merge patch.

## IoK8SApiCoreV1NodeSelectorTerm
### Properties
* **matchExpressions**: [IoK8SApiCoreV1NodeSelectorRequirement](#iok8sapicorev1nodeselectorrequirement)[]: A list of node selector requirements by node's labels.
* **matchFields**: [IoK8SApiCoreV1NodeSelectorRequirement](#iok8sapicorev1nodeselectorrequirement)[]: A list of node selector requirements by node's fields.

## IoK8SApiCoreV1ObjectFieldSelector
### Properties
* **apiVersion**: string: Version of the schema the FieldPath is written in terms of, defaults to "v1".
* **fieldPath**: string (Required): Path of the field to select in the specified API version.

## IoK8SApiCoreV1ObjectReference
### Properties
* **apiVersion**: string: API version of the referent.
* **fieldPath**: string: If referring to a piece of an object instead of an entire object, this string should contain a valid JSON/Go field access statement, such as desiredState.manifest.containers[2]. For example, if the object reference is to a container within a pod, this would take on a value like: "spec.containers{name}" (where "name" refers to the name of the container that triggered the event) or if no container name is specified "spec.containers[2]" (container with index 2 in this pod). This syntax is chosen only to have some well-defined way of referencing a part of an object.
* **kind**: string: Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **namespace**: string: Namespace of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
* **resourceVersion**: string: Specific resourceVersion to which this reference is made, if any. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#concurrency-control-and-consistency
* **uid**: string: UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#uids

## IoK8SApiCoreV1PersistentVolumeClaimSpec
### Properties
* **accessModes**: string[]: accessModes contains the desired access modes the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes-1
* **dataSource**: [IoK8SApiCoreV1TypedLocalObjectReference](#iok8sapicorev1typedlocalobjectreference): TypedLocalObjectReference contains enough information to let you locate the typed referenced object inside the same namespace.
* **dataSourceRef**: [IoK8SApiCoreV1TypedLocalObjectReference](#iok8sapicorev1typedlocalobjectreference): TypedLocalObjectReference contains enough information to let you locate the typed referenced object inside the same namespace.
* **resources**: [IoK8SApiCoreV1ResourceRequirements](#iok8sapicorev1resourcerequirements): ResourceRequirements describes the compute resource requirements.
* **selector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **storageClassName**: string: storageClassName is the name of the StorageClass required by the claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#class-1
* **volumeMode**: string: volumeMode defines what type of volume is required by the claim. Value of Filesystem is implied when not included in claim spec.
* **volumeName**: string: volumeName is the binding reference to the PersistentVolume backing this claim.

## IoK8SApiCoreV1PersistentVolumeClaimTemplate
### Properties
* **metadata**: [IoK8SApimachineryPkgApisMetaV1ObjectMeta](#iok8sapimachinerypkgapismetav1objectmeta): ObjectMeta is metadata that all persisted resources must have, which includes all objects users must create.
* **spec**: [IoK8SApiCoreV1PersistentVolumeClaimSpec](#iok8sapicorev1persistentvolumeclaimspec) (Required): PersistentVolumeClaimSpec describes the common attributes of storage devices and allows a Source for provider-specific attributes

## IoK8SApiCoreV1PersistentVolumeClaimVolumeSource
### Properties
* **claimName**: string (Required): claimName is the name of a PersistentVolumeClaim in the same namespace as the pod using this volume. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
* **readOnly**: bool: readOnly Will force the ReadOnly setting in VolumeMounts. Default false.

## IoK8SApiCoreV1PhotonPersistentDiskVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
* **pdID**: string (Required): pdID is the ID that identifies Photon Controller persistent disk

## IoK8SApiCoreV1PodAffinity
### Properties
* **preferredDuringSchedulingIgnoredDuringExecution**: [IoK8SApiCoreV1WeightedPodAffinityTerm](#iok8sapicorev1weightedpodaffinityterm)[]: The scheduler will prefer to schedule pods to nodes that satisfy the affinity expressions specified by this field, but it may choose a node that violates one or more of the expressions. The node that is most preferred is the one with the greatest sum of weights, i.e. for each node that meets all of the scheduling requirements (resource request, requiredDuringScheduling affinity expressions, etc.), compute a sum by iterating through the elements of this field and adding "weight" to the sum if the node has pods which matches the corresponding podAffinityTerm; the node(s) with the highest sum are the most preferred.
* **requiredDuringSchedulingIgnoredDuringExecution**: [IoK8SApiCoreV1PodAffinityTerm](#iok8sapicorev1podaffinityterm)[]: If the affinity requirements specified by this field are not met at scheduling time, the pod will not be scheduled onto the node. If the affinity requirements specified by this field cease to be met at some point during pod execution (e.g. due to a pod label update), the system may or may not try to eventually evict the pod from its node. When there are multiple elements, the lists of nodes corresponding to each podAffinityTerm are intersected, i.e. all terms must be satisfied.

## IoK8SApiCoreV1PodAffinityTerm
### Properties
* **labelSelector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **namespaces**: string[]: namespaces specifies a static list of namespace names that the term applies to. The term is applied to the union of the namespaces listed in this field and the ones selected by namespaceSelector. null or empty namespaces list and null namespaceSelector means "this pod's namespace".
* **namespaceSelector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **topologyKey**: string (Required): This pod should be co-located (affinity) or not co-located (anti-affinity) with the pods matching the labelSelector in the specified namespaces, where co-located is defined as running on a node whose value of the label with key topologyKey matches that of any node on which any of the selected pods is running. Empty topologyKey is not allowed.

## IoK8SApiCoreV1PodAntiAffinity
### Properties
* **preferredDuringSchedulingIgnoredDuringExecution**: [IoK8SApiCoreV1WeightedPodAffinityTerm](#iok8sapicorev1weightedpodaffinityterm)[]: The scheduler will prefer to schedule pods to nodes that satisfy the anti-affinity expressions specified by this field, but it may choose a node that violates one or more of the expressions. The node that is most preferred is the one with the greatest sum of weights, i.e. for each node that meets all of the scheduling requirements (resource request, requiredDuringScheduling anti-affinity expressions, etc.), compute a sum by iterating through the elements of this field and adding "weight" to the sum if the node has pods which matches the corresponding podAffinityTerm; the node(s) with the highest sum are the most preferred.
* **requiredDuringSchedulingIgnoredDuringExecution**: [IoK8SApiCoreV1PodAffinityTerm](#iok8sapicorev1podaffinityterm)[]: If the anti-affinity requirements specified by this field are not met at scheduling time, the pod will not be scheduled onto the node. If the anti-affinity requirements specified by this field cease to be met at some point during pod execution (e.g. due to a pod label update), the system may or may not try to eventually evict the pod from its node. When there are multiple elements, the lists of nodes corresponding to each podAffinityTerm are intersected, i.e. all terms must be satisfied.

## IoK8SApiCoreV1PodDNSConfig
### Properties
* **nameservers**: string[]: A list of DNS name server IP addresses. This will be appended to the base nameservers generated from DNSPolicy. Duplicated nameservers will be removed.
* **options**: [IoK8SApiCoreV1PodDNSConfigOption](#iok8sapicorev1poddnsconfigoption)[]: A list of DNS resolver options. This will be merged with the base options generated from DNSPolicy. Duplicated entries will be removed. Resolution options given in Options will override those that appear in the base DNSPolicy.
* **searches**: string[]: A list of DNS search domains for host-name lookup. This will be appended to the base search paths generated from DNSPolicy. Duplicated search paths will be removed.

## IoK8SApiCoreV1PodDNSConfigOption
### Properties
* **name**: string: Required.
* **value**: string

## IoK8SApiCoreV1PodOS
### Properties
* **name**: string (Required): Name is the name of the operating system. The currently supported values are linux and windows. Additional value may be defined in future and can be one of: https://github.com/opencontainers/runtime-spec/blob/master/config.md#platform-specific-configuration Clients should expect to handle additional values and treat unrecognized values in this field as os: null

## IoK8SApiCoreV1PodReadinessGate
### Properties
* **conditionType**: string (Required): ConditionType refers to a condition in the pod's condition list with matching type.

## IoK8SApiCoreV1PodSecurityContext
### Properties
* **fsGroup**: int: A special supplemental group that applies to all containers in a pod. Some volume types allow the Kubelet to change the ownership of that volume to be owned by the pod:

1. The owning GID will be the FSGroup 2. The setgid bit is set (new files created in the volume will be owned by FSGroup) 3. The permission bits are OR'd with rw-rw----

If unset, the Kubelet will not modify the ownership and permissions of any volume. Note that this field cannot be set when spec.os.name is windows.
* **fsGroupChangePolicy**: string: fsGroupChangePolicy defines behavior of changing ownership and permission of the volume before being exposed inside Pod. This field will only apply to volume types which support fsGroup based ownership(and permissions). It will have no effect on ephemeral volume types such as: secret, configmaps and emptydir. Valid values are "OnRootMismatch" and "Always". If not specified, "Always" is used. Note that this field cannot be set when spec.os.name is windows.
* **runAsGroup**: int: The GID to run the entrypoint of the container process. Uses runtime default if unset. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container. Note that this field cannot be set when spec.os.name is windows.
* **runAsNonRoot**: bool: Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
* **runAsUser**: int: The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container. Note that this field cannot be set when spec.os.name is windows.
* **seccompProfile**: [IoK8SApiCoreV1SeccompProfile](#iok8sapicorev1seccompprofile): SeccompProfile defines a pod/container's seccomp profile settings. Only one profile source may be set.
* **seLinuxOptions**: [IoK8SApiCoreV1SELinuxOptions](#iok8sapicorev1selinuxoptions): SELinuxOptions are the labels to be applied to the container
* **supplementalGroups**: int[]: A list of groups applied to the first process run in each container, in addition to the container's primary GID.  If unspecified, no groups will be added to any container. Note that this field cannot be set when spec.os.name is windows.
* **sysctls**: [IoK8SApiCoreV1Sysctl](#iok8sapicorev1sysctl)[]: Sysctls hold a list of namespaced sysctls used for the pod. Pods with unsupported sysctls (by the container runtime) might fail to launch. Note that this field cannot be set when spec.os.name is windows.
* **windowsOptions**: [IoK8SApiCoreV1WindowsSecurityContextOptions](#iok8sapicorev1windowssecuritycontextoptions): WindowsSecurityContextOptions contain Windows-specific options and credentials.

## IoK8SApiCoreV1PodSpec
### Properties
* **activeDeadlineSeconds**: int: Optional duration in seconds the pod may be active on the node relative to StartTime before the system will actively try to mark it failed and kill associated containers. Value must be a positive integer.
* **affinity**: [IoK8SApiCoreV1Affinity](#iok8sapicorev1affinity): Affinity is a group of affinity scheduling rules.
* **automountServiceAccountToken**: bool: AutomountServiceAccountToken indicates whether a service account token should be automatically mounted.
* **containers**: [IoK8SApiCoreV1Container](#iok8sapicorev1container)[] (Required): List of containers belonging to the pod. Containers cannot currently be added or removed. There must be at least one container in a Pod. Cannot be updated.
* **dnsConfig**: [IoK8SApiCoreV1PodDNSConfig](#iok8sapicorev1poddnsconfig): PodDNSConfig defines the DNS parameters of a pod in addition to those generated from DNSPolicy.
* **dnsPolicy**: string: Set DNS policy for the pod. Defaults to "ClusterFirst". Valid values are 'ClusterFirstWithHostNet', 'ClusterFirst', 'Default' or 'None'. DNS parameters given in DNSConfig will be merged with the policy selected with DNSPolicy. To have DNS options set along with hostNetwork, you have to specify DNS policy explicitly to 'ClusterFirstWithHostNet'.
* **enableServiceLinks**: bool: EnableServiceLinks indicates whether information about services should be injected into pod's environment variables, matching the syntax of Docker links. Optional: Defaults to true.
* **ephemeralContainers**: [IoK8SApiCoreV1EphemeralContainer](#iok8sapicorev1ephemeralcontainer)[]: List of ephemeral containers run in this pod. Ephemeral containers may be run in an existing pod to perform user-initiated actions such as debugging. This list cannot be specified when creating a pod, and it cannot be modified by updating the pod spec. In order to add an ephemeral container to an existing pod, use the pod's ephemeralcontainers subresource.
* **hostAliases**: [IoK8SApiCoreV1HostAlias](#iok8sapicorev1hostalias)[]: HostAliases is an optional list of hosts and IPs that will be injected into the pod's hosts file if specified. This is only valid for non-hostNetwork pods.
* **hostIPC**: bool: Use the host's ipc namespace. Optional: Default to false.
* **hostname**: string: Specifies the hostname of the Pod If not specified, the pod's hostname will be set to a system-defined value.
* **hostNetwork**: bool: Host networking requested for this pod. Use the host's network namespace. If this option is set, the ports that will be used must be specified. Default to false.
* **hostPID**: bool: Use the host's pid namespace. Optional: Default to false.
* **hostUsers**: bool: Use the host's user namespace. Optional: Default to true. If set to true or not present, the pod will be run in the host user namespace, useful for when the pod needs a feature only available to the host user namespace, such as loading a kernel module with CAP_SYS_MODULE. When set to false, a new userns is created for the pod. Setting false is useful for mitigating container breakout vulnerabilities even allowing users to run their containers as root without actually having root privileges on the host. This field is alpha-level and is only honored by servers that enable the UserNamespacesSupport feature.
* **imagePullSecrets**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference)[]: ImagePullSecrets is an optional list of references to secrets in the same namespace to use for pulling any of the images used by this PodSpec. If specified, these secrets will be passed to individual puller implementations for them to use. More info: https://kubernetes.io/docs/concepts/containers/images#specifying-imagepullsecrets-on-a-pod
* **initContainers**: [IoK8SApiCoreV1Container](#iok8sapicorev1container)[]: List of initialization containers belonging to the pod. Init containers are executed in order prior to containers being started. If any init container fails, the pod is considered to have failed and is handled according to its restartPolicy. The name for an init container or normal container must be unique among all containers. Init containers may not have Lifecycle actions, Readiness probes, Liveness probes, or Startup probes. The resourceRequirements of an init container are taken into account during scheduling by finding the highest request/limit for each resource type, and then using the max of of that value or the sum of the normal containers. Limits are applied to init containers in a similar fashion. Init containers cannot currently be added or removed. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/init-containers/
* **nodeName**: string: NodeName is a request to schedule this pod onto a specific node. If it is non-empty, the scheduler simply schedules this pod onto that node, assuming that it fits resource requirements.
* **nodeSelector**: [IoK8SApiCoreV1PodSpecNodeSelector](#iok8sapicorev1podspecnodeselector): NodeSelector is a selector which must be true for the pod to fit on a node. Selector which must match a node's labels for the pod to be scheduled on that node. More info: https://kubernetes.io/docs/concepts/configuration/assign-pod-node/
* **os**: [IoK8SApiCoreV1PodOS](#iok8sapicorev1podos): PodOS defines the OS parameters of a pod.
* **overhead**: [IoK8SApiCoreV1PodSpecOverhead](#iok8sapicorev1podspecoverhead): Overhead represents the resource overhead associated with running a pod for a given RuntimeClass. This field will be autopopulated at admission time by the RuntimeClass admission controller. If the RuntimeClass admission controller is enabled, overhead must not be set in Pod create requests. The RuntimeClass admission controller will reject Pod create requests which have the overhead already set. If RuntimeClass is configured and selected in the PodSpec, Overhead will be set to the value defined in the corresponding RuntimeClass, otherwise it will remain unset and treated as zero. More info: https://git.k8s.io/enhancements/keps/sig-node/688-pod-overhead/README.md
* **preemptionPolicy**: string: PreemptionPolicy is the Policy for preempting pods with lower priority. One of Never, PreemptLowerPriority. Defaults to PreemptLowerPriority if unset.
* **priority**: int: The priority value. Various system components use this field to find the priority of the pod. When Priority Admission Controller is enabled, it prevents users from setting this field. The admission controller populates this field from PriorityClassName. The higher the value, the higher the priority.
* **priorityClassName**: string: If specified, indicates the pod's priority. "system-node-critical" and "system-cluster-critical" are two special keywords which indicate the highest priorities with the former being the highest priority. Any other name must be defined by creating a PriorityClass object with that name. If not specified, the pod priority will be default or zero if there is no default.
* **readinessGates**: [IoK8SApiCoreV1PodReadinessGate](#iok8sapicorev1podreadinessgate)[]: If specified, all readiness gates will be evaluated for pod readiness. A pod is ready when all its containers are ready AND all conditions specified in the readiness gates have status equal to "True" More info: https://git.k8s.io/enhancements/keps/sig-network/580-pod-readiness-gates
* **restartPolicy**: string: Restart policy for all containers within the pod. One of Always, OnFailure, Never. Default to Always. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle/#restart-policy
* **runtimeClassName**: string: RuntimeClassName refers to a RuntimeClass object in the node.k8s.io group, which should be used to run this pod.  If no RuntimeClass resource matches the named class, the pod will not be run. If unset or empty, the "legacy" RuntimeClass will be used, which is an implicit class with an empty definition that uses the default runtime handler. More info: https://git.k8s.io/enhancements/keps/sig-node/585-runtime-class
* **schedulerName**: string: If specified, the pod will be dispatched by specified scheduler. If not specified, the pod will be dispatched by default scheduler.
* **securityContext**: [IoK8SApiCoreV1PodSecurityContext](#iok8sapicorev1podsecuritycontext): PodSecurityContext holds pod-level security attributes and common container settings. Some fields are also present in container.securityContext.  Field values of container.securityContext take precedence over field values of PodSecurityContext.
* **serviceAccount**: string: DeprecatedServiceAccount is a depreciated alias for ServiceAccountName. Deprecated: Use serviceAccountName instead.
* **serviceAccountName**: string: ServiceAccountName is the name of the ServiceAccount to use to run this pod. More info: https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/
* **setHostnameAsFQDN**: bool: If true the pod's hostname will be configured as the pod's FQDN, rather than the leaf name (the default). In Linux containers, this means setting the FQDN in the hostname field of the kernel (the nodename field of struct utsname). In Windows containers, this means setting the registry value of hostname for the registry key HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters to FQDN. If a pod does not have FQDN, this has no effect. Default to false.
* **shareProcessNamespace**: bool: Share a single process namespace between all of the containers in a pod. When this is set containers will be able to view and signal processes from other containers in the same pod, and the first process in each container will not be assigned PID 1. HostPID and ShareProcessNamespace cannot both be set. Optional: Default to false.
* **subdomain**: string: If specified, the fully qualified Pod hostname will be "<hostname>.<subdomain>.<pod namespace>.svc.<cluster domain>". If not specified, the pod will not have a domainname at all.
* **terminationGracePeriodSeconds**: int: Optional duration in seconds the pod needs to terminate gracefully. May be decreased in delete request. Value must be non-negative integer. The value zero indicates stop immediately via the kill signal (no opportunity to shut down). If this value is nil, the default grace period will be used instead. The grace period is the duration in seconds after the processes running in the pod are sent a termination signal and the time when the processes are forcibly halted with a kill signal. Set this value longer than the expected cleanup time for your process. Defaults to 30 seconds.
* **tolerations**: [IoK8SApiCoreV1Toleration](#iok8sapicorev1toleration)[]: If specified, the pod's tolerations.
* **topologySpreadConstraints**: [IoK8SApiCoreV1TopologySpreadConstraint](#iok8sapicorev1topologyspreadconstraint)[]: TopologySpreadConstraints describes how a group of pods ought to spread across topology domains. Scheduler will schedule pods in a way which abides by the constraints. All topologySpreadConstraints are ANDed.
* **volumes**: [IoK8SApiCoreV1Volume](#iok8sapicorev1volume)[]: List of volumes that can be mounted by containers belonging to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes

## IoK8SApiCoreV1PodSpecNodeSelector
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1PodSpecOverhead
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1PodTemplateSpec
### Properties
* **metadata**: [IoK8SApimachineryPkgApisMetaV1ObjectMeta](#iok8sapimachinerypkgapismetav1objectmeta): ObjectMeta is metadata that all persisted resources must have, which includes all objects users must create.
* **spec**: [IoK8SApiCoreV1PodSpec](#iok8sapicorev1podspec): PodSpec is a description of a pod.

## IoK8SApiCoreV1PortworxVolumeSource
### Properties
* **fsType**: string: fSType represents the filesystem type to mount Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs". Implicitly inferred to be "ext4" if unspecified.
* **readOnly**: bool: readOnly defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
* **volumeID**: string (Required): volumeID uniquely identifies a Portworx volume

## IoK8SApiCoreV1PreferredSchedulingTerm
### Properties
* **preference**: [IoK8SApiCoreV1NodeSelectorTerm](#iok8sapicorev1nodeselectorterm) (Required): A null or empty node selector term matches no objects. The requirements of them are ANDed. The TopologySelectorTerm type implements a subset of the NodeSelectorTerm.
* **weight**: int (Required): Weight associated with matching the corresponding nodeSelectorTerm, in the range 1-100.

## IoK8SApiCoreV1Probe
### Properties
* **exec**: [IoK8SApiCoreV1ExecAction](#iok8sapicorev1execaction): ExecAction describes a "run in container" action.
* **failureThreshold**: int: Minimum consecutive failures for the probe to be considered failed after having succeeded. Defaults to 3. Minimum value is 1.
* **grpc**: [IoK8SApiCoreV1GrpcAction](#iok8sapicorev1grpcaction)
* **httpGet**: [IoK8SApiCoreV1HttpGetAction](#iok8sapicorev1httpgetaction): HTTPGetAction describes an action based on HTTP Get requests.
* **initialDelaySeconds**: int: Number of seconds after the container has started before liveness probes are initiated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
* **periodSeconds**: int: How often (in seconds) to perform the probe. Default to 10 seconds. Minimum value is 1.
* **successThreshold**: int: Minimum consecutive successes for the probe to be considered successful after having failed. Defaults to 1. Must be 1 for liveness and startup. Minimum value is 1.
* **tcpSocket**: [IoK8SApiCoreV1TCPSocketAction](#iok8sapicorev1tcpsocketaction): TCPSocketAction describes an action based on opening a socket
* **terminationGracePeriodSeconds**: int: Optional duration in seconds the pod needs to terminate gracefully upon probe failure. The grace period is the duration in seconds after the processes running in the pod are sent a termination signal and the time when the processes are forcibly halted with a kill signal. Set this value longer than the expected cleanup time for your process. If this value is nil, the pod's terminationGracePeriodSeconds will be used. Otherwise, this value overrides the value provided by the pod spec. Value must be non-negative integer. The value zero indicates stop immediately via the kill signal (no opportunity to shut down). This is a beta field and requires enabling ProbeTerminationGracePeriod feature gate. Minimum value is 1. spec.terminationGracePeriodSeconds is used if unset.
* **timeoutSeconds**: int: Number of seconds after which the probe times out. Defaults to 1 second. Minimum value is 1. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes

## IoK8SApiCoreV1ProjectedVolumeSource
### Properties
* **defaultMode**: int: defaultMode are the mode bits used to set permissions on created files by default. Must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
* **sources**: [IoK8SApiCoreV1VolumeProjection](#iok8sapicorev1volumeprojection)[]: sources is the list of volume projections

## IoK8SApiCoreV1QuobyteVolumeSource
### Properties
* **group**: string: group to map volume access to Default is no group
* **readOnly**: bool: readOnly here will force the Quobyte volume to be mounted with read-only permissions. Defaults to false.
* **registry**: string (Required): registry represents a single or multiple Quobyte Registry services specified as a string as host:port pair (multiple entries are separated with commas) which acts as the central registry for volumes
* **tenant**: string: tenant owning the given Quobyte volume in the Backend Used with dynamically provisioned Quobyte volumes, value is set by the plugin
* **user**: string: user to map volume access to Defaults to serivceaccount user
* **volume**: string (Required): volume is a string that references an already created Quobyte volume by name.

## IoK8SApiCoreV1RBDVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#rbd
* **image**: string (Required): image is the rados image name. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
* **keyring**: string: keyring is the path to key ring for RBDUser. Default is /etc/ceph/keyring. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
* **monitors**: string[] (Required): monitors is a collection of Ceph monitors. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
* **pool**: string: pool is the rados pool name. Default is rbd. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
* **readOnly**: bool: readOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **user**: string: user is the rados user name. Default is admin. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it

## IoK8SApiCoreV1ResourceFieldSelector
### Properties
* **containerName**: string: Container name: required for volumes, optional for env vars
* **divisor**: string: Quantity is a fixed-point representation of a number. It provides convenient marshaling/unmarshaling in JSON and YAML, in addition to String() and AsInt64() accessors.

The serialization format is:

``` <quantity>        ::= <signedNumber><suffix>

	(Note that <suffix> may be empty, from the "" case in <decimalSI>.)

<digit>           ::= 0 | 1 | ... | 9 <digits>          ::= <digit> | <digit><digits> <number>          ::= <digits> | <digits>.<digits> | <digits>. | .<digits> <sign>            ::= "+" | "-" <signedNumber>    ::= <number> | <sign><number> <suffix>          ::= <binarySI> | <decimalExponent> | <decimalSI> <binarySI>        ::= Ki | Mi | Gi | Ti | Pi | Ei

	(International System of units; See: http://physics.nist.gov/cuu/Units/binary.html)

<decimalSI>       ::= m | "" | k | M | G | T | P | E

	(Note that 1024 = 1Ki but 1000 = 1k; I didn't choose the capitalization.)

<decimalExponent> ::= "e" <signedNumber> | "E" <signedNumber> ```

No matter which of the three exponent forms is used, no quantity may represent a number greater than 2^63-1 in magnitude, nor may it have more than 3 decimal places. Numbers larger or more precise will be capped or rounded up. (E.g.: 0.1m will rounded up to 1m.) This may be extended in the future if we require larger or smaller quantities.

When a Quantity is parsed from a string, it will remember the type of suffix it had, and will use the same type again when it is serialized.

Before serializing, Quantity will be put in "canonical form". This means that Exponent/suffix will be adjusted up or down (with a corresponding increase or decrease in Mantissa) such that:

- No precision is lost - No fractional digits will be emitted - The exponent (or suffix) is as large as possible.

The sign will be omitted unless the number is negative.

Examples:

- 1.5 will be serialized as "1500m" - 1.5Gi will be serialized as "1536Mi"

Note that the quantity will NEVER be internally represented by a floating point number. That is the whole point of this exercise.

Non-canonical values will still parse as long as they are well formed, but will be re-emitted in their canonical form. (So always use canonical form, or don't diff.)

This format is intended to make it difficult to use these numbers without writing some sort of special handling code in the hopes that that will cause implementors to also use a fixed point implementation.
* **resource**: string (Required): Required: resource to select

## IoK8SApiCoreV1ResourceRequirements
### Properties
* **limits**: [IoK8SApiCoreV1ResourceRequirementsLimits](#iok8sapicorev1resourcerequirementslimits): Limits describes the maximum amount of compute resources allowed. More info: https://kubernetes.io/docs/concepts/configuration/manage-resources-containers/
* **requests**: [IoK8SApiCoreV1ResourceRequirementsRequests](#iok8sapicorev1resourcerequirementsrequests): Requests describes the minimum amount of compute resources required. If Requests is omitted for a container, it defaults to Limits if that is explicitly specified, otherwise to an implementation-defined value. More info: https://kubernetes.io/docs/concepts/configuration/manage-resources-containers/

## IoK8SApiCoreV1ResourceRequirementsLimits
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1ResourceRequirementsRequests
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApiCoreV1ScaleIOVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Default is "xfs".
* **gateway**: string (Required): gateway is the host address of the ScaleIO API Gateway.
* **protectionDomain**: string: protectionDomain is the name of the ScaleIO Protection Domain for the configured storage.
* **readOnly**: bool: readOnly Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference) (Required): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **sslEnabled**: bool: sslEnabled Flag enable/disable SSL communication with Gateway, default false
* **storageMode**: string: storageMode indicates whether the storage for a volume should be ThickProvisioned or ThinProvisioned. Default is ThinProvisioned.
* **storagePool**: string: storagePool is the ScaleIO Storage Pool associated with the protection domain.
* **system**: string (Required): system is the name of the storage system as configured in ScaleIO.
* **volumeName**: string: volumeName is the name of a volume already created in the ScaleIO system that is associated with this volume source.

## IoK8SApiCoreV1SeccompProfile
### Properties
* **localhostProfile**: string: localhostProfile indicates a profile defined in a file on the node should be used. The profile must be preconfigured on the node to work. Must be a descending path, relative to the kubelet's configured seccomp profile location. Must only be set if type is "Localhost".
* **type**: string (Required): type indicates which kind of seccomp profile will be applied. Valid options are:

Localhost - a profile defined in a file on the node should be used. RuntimeDefault - the container runtime default profile should be used. Unconfined - no profile should be applied.

## IoK8SApiCoreV1SecretEnvSource
### Properties
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: Specify whether the Secret must be defined

## IoK8SApiCoreV1SecretKeySelector
### Properties
* **key**: string (Required): The key of the secret to select from.  Must be a valid secret key.
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: Specify whether the Secret or its key must be defined

## IoK8SApiCoreV1SecretProjection
### Properties
* **items**: [IoK8SApiCoreV1KeyToPath](#iok8sapicorev1keytopath)[]: items if unspecified, each key-value pair in the Data field of the referenced Secret will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the Secret, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
* **name**: string: Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **optional**: bool: optional field specify whether the Secret or its key must be defined

## IoK8SApiCoreV1SecretVolumeSource
### Properties
* **defaultMode**: int: defaultMode is Optional: mode bits used to set permissions on created files by default. Must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
* **items**: [IoK8SApiCoreV1KeyToPath](#iok8sapicorev1keytopath)[]: items If unspecified, each key-value pair in the Data field of the referenced Secret will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the Secret, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
* **optional**: bool: optional field specify whether the Secret or its keys must be defined
* **secretName**: string: secretName is the name of the secret in the pod's namespace to use. More info: https://kubernetes.io/docs/concepts/storage/volumes#secret

## IoK8SApiCoreV1SecurityContext
### Properties
* **allowPrivilegeEscalation**: bool: AllowPrivilegeEscalation controls whether a process can gain more privileges than its parent process. This bool directly controls if the no_new_privs flag will be set on the container process. AllowPrivilegeEscalation is true always when the container is: 1) run as Privileged 2) has CAP_SYS_ADMIN Note that this field cannot be set when spec.os.name is windows.
* **capabilities**: [IoK8SApiCoreV1Capabilities](#iok8sapicorev1capabilities): Adds and removes POSIX capabilities from running containers.
* **privileged**: bool: Run container in privileged mode. Processes in privileged containers are essentially equivalent to root on the host. Defaults to false. Note that this field cannot be set when spec.os.name is windows.
* **procMount**: string: procMount denotes the type of proc mount to use for the containers. The default is DefaultProcMount which uses the container runtime defaults for readonly paths and masked paths. This requires the ProcMountType feature flag to be enabled. Note that this field cannot be set when spec.os.name is windows.
* **readOnlyRootFilesystem**: bool: Whether this container has a read-only root filesystem. Default is false. Note that this field cannot be set when spec.os.name is windows.
* **runAsGroup**: int: The GID to run the entrypoint of the container process. Uses runtime default if unset. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is windows.
* **runAsNonRoot**: bool: Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
* **runAsUser**: int: The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is windows.
* **seccompProfile**: [IoK8SApiCoreV1SeccompProfile](#iok8sapicorev1seccompprofile): SeccompProfile defines a pod/container's seccomp profile settings. Only one profile source may be set.
* **seLinuxOptions**: [IoK8SApiCoreV1SELinuxOptions](#iok8sapicorev1selinuxoptions): SELinuxOptions are the labels to be applied to the container
* **windowsOptions**: [IoK8SApiCoreV1WindowsSecurityContextOptions](#iok8sapicorev1windowssecuritycontextoptions): WindowsSecurityContextOptions contain Windows-specific options and credentials.

## IoK8SApiCoreV1SELinuxOptions
### Properties
* **level**: string: Level is SELinux level label that applies to the container.
* **role**: string: Role is a SELinux role label that applies to the container.
* **type**: string: Type is a SELinux type label that applies to the container.
* **user**: string: User is a SELinux user label that applies to the container.

## IoK8SApiCoreV1ServiceAccountTokenProjection
### Properties
* **audience**: string: audience is the intended audience of the token. A recipient of a token must identify itself with an identifier specified in the audience of the token, and otherwise should reject the token. The audience defaults to the identifier of the apiserver.
* **expirationSeconds**: int: expirationSeconds is the requested duration of validity of the service account token. As the token approaches expiration, the kubelet volume plugin will proactively rotate the service account token. The kubelet will start trying to rotate the token if the token is older than 80 percent of its time to live or if the token is older than 24 hours.Defaults to 1 hour and must be at least 10 minutes.
* **path**: string (Required): path is the path relative to the mount point of the file to project the token into.

## IoK8SApiCoreV1StorageOSVolumeSource
### Properties
* **fsType**: string: fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
* **readOnly**: bool: readOnly defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
* **secretRef**: [IoK8SApiCoreV1LocalObjectReference](#iok8sapicorev1localobjectreference): LocalObjectReference contains enough information to let you locate the referenced object inside the same namespace.
* **volumeName**: string: volumeName is the human-readable name of the StorageOS volume.  Volume names are only unique within a namespace.
* **volumeNamespace**: string: volumeNamespace specifies the scope of the volume within StorageOS.  If no namespace is specified then the Pod's namespace will be used.  This allows the Kubernetes name scoping to be mirrored within StorageOS for tighter integration. Set VolumeName to any name to override the default behaviour. Set to "default" if you are not using namespaces within StorageOS. Namespaces that do not pre-exist within StorageOS will be created.

## IoK8SApiCoreV1Sysctl
### Properties
* **name**: string (Required): Name of a property to set
* **value**: string (Required): Value of a property to set

## IoK8SApiCoreV1TCPSocketAction
### Properties
* **host**: string: Optional: Host name to connect to, defaults to the pod IP.
* **port**: string (Required): IntOrString is a type that can hold an int32 or a string.  When used in JSON or YAML marshalling and unmarshalling, it produces or consumes the inner type.  This allows you to have, for example, a JSON field that can accept a name or number.

## IoK8SApiCoreV1Toleration
### Properties
* **effect**: string: Effect indicates the taint effect to match. Empty means match all taint effects. When specified, allowed values are NoSchedule, PreferNoSchedule and NoExecute.
* **key**: string: Key is the taint key that the toleration applies to. Empty means match all taint keys. If the key is empty, operator must be Exists; this combination means to match all values and all keys.
* **operator**: string: Operator represents a key's relationship to the value. Valid operators are Exists and Equal. Defaults to Equal. Exists is equivalent to wildcard for value, so that a pod can tolerate all taints of a particular category.
* **tolerationSeconds**: int: TolerationSeconds represents the period of time the toleration (which must be of effect NoExecute, otherwise this field is ignored) tolerates the taint. By default, it is not set, which means tolerate the taint forever (do not evict). Zero and negative values will be treated as 0 (evict immediately) by the system.
* **value**: string: Value is the taint value the toleration matches to. If the operator is Exists, the value should be empty, otherwise just a regular string.

## IoK8SApiCoreV1TopologySpreadConstraint
### Properties
* **labelSelector**: [IoK8SApimachineryPkgApisMetaV1LabelSelector](#iok8sapimachinerypkgapismetav1labelselector): A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
* **matchLabelKeys**: string[]: MatchLabelKeys is a set of pod label keys to select the pods over which spreading will be calculated. The keys are used to lookup values from the incoming pod labels, those key-value labels are ANDed with labelSelector to select the group of existing pods over which spreading will be calculated for the incoming pod. Keys that don't exist in the incoming pod labels will be ignored. A null or empty list means only match against labelSelector.
* **maxSkew**: int (Required): MaxSkew describes the degree to which pods may be unevenly distributed. When `whenUnsatisfiable=DoNotSchedule`, it is the maximum permitted difference between the number of matching pods in the target topology and the global minimum. The global minimum is the minimum number of matching pods in an eligible domain or zero if the number of eligible domains is less than MinDomains. For example, in a 3-zone cluster, MaxSkew is set to 1, and pods with the same labelSelector spread as 2/2/1: In this case, the global minimum is 1. | zone1 | zone2 | zone3 | |  P P  |  P P  |   P   | - if MaxSkew is 1, incoming pod can only be scheduled to zone3 to become 2/2/2; scheduling it onto zone1(zone2) would make the ActualSkew(3-1) on zone1(zone2) violate MaxSkew(1). - if MaxSkew is 2, incoming pod can be scheduled onto any zone. When `whenUnsatisfiable=ScheduleAnyway`, it is used to give higher precedence to topologies that satisfy it. It's a required field. Default value is 1 and 0 is not allowed.
* **minDomains**: int: MinDomains indicates a minimum number of eligible domains. When the number of eligible domains with matching topology keys is less than minDomains, Pod Topology Spread treats "global minimum" as 0, and then the calculation of Skew is performed. And when the number of eligible domains with matching topology keys equals or greater than minDomains, this value has no effect on scheduling. As a result, when the number of eligible domains is less than minDomains, scheduler won't schedule more than maxSkew Pods to those domains. If value is nil, the constraint behaves as if MinDomains is equal to 1. Valid values are integers greater than 0. When value is not nil, WhenUnsatisfiable must be DoNotSchedule.

For example, in a 3-zone cluster, MaxSkew is set to 2, MinDomains is set to 5 and pods with the same labelSelector spread as 2/2/2: | zone1 | zone2 | zone3 | |  P P  |  P P  |  P P  | The number of domains is less than 5(MinDomains), so "global minimum" is treated as 0. In this situation, new pod with the same labelSelector cannot be scheduled, because computed skew will be 3(3 - 0) if new Pod is scheduled to any of the three zones, it will violate MaxSkew.

This is a beta field and requires the MinDomainsInPodTopologySpread feature gate to be enabled (enabled by default).
* **nodeAffinityPolicy**: string: NodeAffinityPolicy indicates how we will treat Pod's nodeAffinity/nodeSelector when calculating pod topology spread skew. Options are: - Honor: only nodes matching nodeAffinity/nodeSelector are included in the calculations. - Ignore: nodeAffinity/nodeSelector are ignored. All nodes are included in the calculations.

If this value is nil, the behavior is equivalent to the Honor policy. This is a alpha-level feature enabled by the NodeInclusionPolicyInPodTopologySpread feature flag.
* **nodeTaintsPolicy**: string: NodeTaintsPolicy indicates how we will treat node taints when calculating pod topology spread skew. Options are: - Honor: nodes without taints, along with tainted nodes for which the incoming pod has a toleration, are included. - Ignore: node taints are ignored. All nodes are included.

If this value is nil, the behavior is equivalent to the Ignore policy. This is a alpha-level feature enabled by the NodeInclusionPolicyInPodTopologySpread feature flag.
* **topologyKey**: string (Required): TopologyKey is the key of node labels. Nodes that have a label with this key and identical values are considered to be in the same topology. We consider each <key, value> as a "bucket", and try to put balanced number of pods into each bucket. We define a domain as a particular instance of a topology. Also, we define an eligible domain as a domain whose nodes meet the requirements of nodeAffinityPolicy and nodeTaintsPolicy. e.g. If TopologyKey is "kubernetes.io/hostname", each Node is a domain of that topology. And, if TopologyKey is "topology.kubernetes.io/zone", each zone is a domain of that topology. It's a required field.
* **whenUnsatisfiable**: string (Required): WhenUnsatisfiable indicates how to deal with a pod if it doesn't satisfy the spread constraint. - DoNotSchedule (default) tells the scheduler not to schedule it. - ScheduleAnyway tells the scheduler to schedule the pod in any location,
  but giving higher precedence to topologies that would help reduce the
  skew.
A constraint is considered "Unsatisfiable" for an incoming pod if and only if every possible node assignment for that pod would violate "MaxSkew" on some topology. For example, in a 3-zone cluster, MaxSkew is set to 1, and pods with the same labelSelector spread as 3/1/1: | zone1 | zone2 | zone3 | | P P P |   P   |   P   | If WhenUnsatisfiable is set to DoNotSchedule, incoming pod can only be scheduled to zone2(zone3) to become 3/2/1(3/1/2) as ActualSkew(2-1) on zone2(zone3) satisfies MaxSkew(1). In other words, the cluster can still be imbalanced, but scheduler won't make it *more* imbalanced. It's a required field.

## IoK8SApiCoreV1TypedLocalObjectReference
### Properties
* **apiGroup**: string: APIGroup is the group for the resource being referenced. If APIGroup is not specified, the specified Kind must be in the core API group. For any other third-party types, APIGroup is required.
* **kind**: string (Required): Kind is the type of resource being referenced
* **name**: string (Required): Name is the name of resource being referenced

## IoK8SApiCoreV1Volume
### Properties
* **awsElasticBlockStore**: [IoK8SApiCoreV1AWSElasticBlockStoreVolumeSource](#iok8sapicorev1awselasticblockstorevolumesource): Represents a Persistent Disk resource in AWS.

An AWS EBS disk must exist before mounting to a container. The disk must also be in the same AWS zone as the kubelet. An AWS EBS disk can only be mounted as read/write once. AWS EBS volumes support ownership management and SELinux relabeling.
* **azureDisk**: [IoK8SApiCoreV1AzureDiskVolumeSource](#iok8sapicorev1azurediskvolumesource): AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
* **azureFile**: [IoK8SApiCoreV1AzureFileVolumeSource](#iok8sapicorev1azurefilevolumesource): AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
* **cephfs**: [IoK8SApiCoreV1CephFSVolumeSource](#iok8sapicorev1cephfsvolumesource): Represents a Ceph Filesystem mount that lasts the lifetime of a pod Cephfs volumes do not support ownership management or SELinux relabeling.
* **cinder**: [IoK8SApiCoreV1CinderVolumeSource](#iok8sapicorev1cindervolumesource): Represents a cinder volume resource in Openstack. A Cinder volume must exist before mounting to a container. The volume must also be in the same region as the kubelet. Cinder volumes support ownership management and SELinux relabeling.
* **configMap**: [IoK8SApiCoreV1ConfigMapVolumeSource](#iok8sapicorev1configmapvolumesource): Adapts a ConfigMap into a volume.

The contents of the target ConfigMap's Data field will be presented in a volume as files using the keys in the Data field as the file names, unless the items element is populated with specific mappings of keys to paths. ConfigMap volumes support ownership management and SELinux relabeling.
* **csi**: [IoK8SApiCoreV1CSIVolumeSource](#iok8sapicorev1csivolumesource): Represents a source location of a volume to mount, managed by an external CSI driver
* **downwardAPI**: [IoK8SApiCoreV1DownwardAPIVolumeSource](#iok8sapicorev1downwardapivolumesource): DownwardAPIVolumeSource represents a volume containing downward API info. Downward API volumes support ownership management and SELinux relabeling.
* **emptyDir**: [IoK8SApiCoreV1EmptyDirVolumeSource](#iok8sapicorev1emptydirvolumesource): Represents an empty directory for a pod. Empty directory volumes support ownership management and SELinux relabeling.
* **ephemeral**: [IoK8SApiCoreV1EphemeralVolumeSource](#iok8sapicorev1ephemeralvolumesource): Represents an ephemeral volume that is handled by a normal storage driver.
* **fc**: [IoK8SApiCoreV1FCVolumeSource](#iok8sapicorev1fcvolumesource): Represents a Fibre Channel volume. Fibre Channel volumes can only be mounted as read/write once. Fibre Channel volumes support ownership management and SELinux relabeling.
* **flexVolume**: [IoK8SApiCoreV1FlexVolumeSource](#iok8sapicorev1flexvolumesource): FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin.
* **flocker**: [IoK8SApiCoreV1FlockerVolumeSource](#iok8sapicorev1flockervolumesource): Represents a Flocker volume mounted by the Flocker agent. One and only one of datasetName and datasetUUID should be set. Flocker volumes do not support ownership management or SELinux relabeling.
* **gcePersistentDisk**: [IoK8SApiCoreV1GCEPersistentDiskVolumeSource](#iok8sapicorev1gcepersistentdiskvolumesource): Represents a Persistent Disk resource in Google Compute Engine.

A GCE PD must exist before mounting to a container. The disk must also be in the same GCE project and zone as the kubelet. A GCE PD can only be mounted as read/write once or read-only many times. GCE PDs support ownership management and SELinux relabeling.
* **gitRepo**: [IoK8SApiCoreV1GitRepoVolumeSource](#iok8sapicorev1gitrepovolumesource): Represents a volume that is populated with the contents of a git repository. Git repo volumes do not support ownership management. Git repo volumes support SELinux relabeling.

DEPRECATED: GitRepo is deprecated. To provision a container with a git repo, mount an EmptyDir into an InitContainer that clones the repo using git, then mount the EmptyDir into the Pod's container.
* **glusterfs**: [IoK8SApiCoreV1GlusterfsVolumeSource](#iok8sapicorev1glusterfsvolumesource): Represents a Glusterfs mount that lasts the lifetime of a pod. Glusterfs volumes do not support ownership management or SELinux relabeling.
* **hostPath**: [IoK8SApiCoreV1HostPathVolumeSource](#iok8sapicorev1hostpathvolumesource): Represents a host path mapped into a pod. Host path volumes do not support ownership management or SELinux relabeling.
* **iscsi**: [IoK8SApiCoreV1IscsiVolumeSource](#iok8sapicorev1iscsivolumesource): Represents an ISCSI disk. ISCSI volumes can only be mounted as read/write once. ISCSI volumes support ownership management and SELinux relabeling.
* **name**: string (Required): name of the volume. Must be a DNS_LABEL and unique within the pod. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
* **nfs**: [IoK8SApiCoreV1NFSVolumeSource](#iok8sapicorev1nfsvolumesource): Represents an NFS mount that lasts the lifetime of a pod. NFS volumes do not support ownership management or SELinux relabeling.
* **persistentVolumeClaim**: [IoK8SApiCoreV1PersistentVolumeClaimVolumeSource](#iok8sapicorev1persistentvolumeclaimvolumesource): PersistentVolumeClaimVolumeSource references the user's PVC in the same namespace. This volume finds the bound PV and mounts that volume for the pod. A PersistentVolumeClaimVolumeSource is, essentially, a wrapper around another type of volume that is owned by someone else (the system).
* **photonPersistentDisk**: [IoK8SApiCoreV1PhotonPersistentDiskVolumeSource](#iok8sapicorev1photonpersistentdiskvolumesource): Represents a Photon Controller persistent disk resource.
* **portworxVolume**: [IoK8SApiCoreV1PortworxVolumeSource](#iok8sapicorev1portworxvolumesource): PortworxVolumeSource represents a Portworx volume resource.
* **projected**: [IoK8SApiCoreV1ProjectedVolumeSource](#iok8sapicorev1projectedvolumesource): Represents a projected volume source
* **quobyte**: [IoK8SApiCoreV1QuobyteVolumeSource](#iok8sapicorev1quobytevolumesource): Represents a Quobyte mount that lasts the lifetime of a pod. Quobyte volumes do not support ownership management or SELinux relabeling.
* **rbd**: [IoK8SApiCoreV1RBDVolumeSource](#iok8sapicorev1rbdvolumesource): Represents a Rados Block Device mount that lasts the lifetime of a pod. RBD volumes support ownership management and SELinux relabeling.
* **scaleIO**: [IoK8SApiCoreV1ScaleIOVolumeSource](#iok8sapicorev1scaleiovolumesource): ScaleIOVolumeSource represents a persistent ScaleIO volume
* **secret**: [IoK8SApiCoreV1SecretVolumeSource](#iok8sapicorev1secretvolumesource): Adapts a Secret into a volume.

The contents of the target Secret's Data field will be presented in a volume as files using the keys in the Data field as the file names. Secret volumes support ownership management and SELinux relabeling.
* **storageos**: [IoK8SApiCoreV1StorageOSVolumeSource](#iok8sapicorev1storageosvolumesource): Represents a StorageOS persistent volume resource.
* **vsphereVolume**: [IoK8SApiCoreV1VsphereVirtualDiskVolumeSource](#iok8sapicorev1vspherevirtualdiskvolumesource): Represents a vSphere volume resource.

## IoK8SApiCoreV1VolumeDevice
### Properties
* **devicePath**: string (Required): devicePath is the path inside of the container that the device will be mapped to.
* **name**: string (Required): name must match the name of a persistentVolumeClaim in the pod

## IoK8SApiCoreV1VolumeMount
### Properties
* **mountPath**: string (Required): Path within the container at which the volume should be mounted.  Must not contain ':'.
* **mountPropagation**: string: mountPropagation determines how mounts are propagated from the host to container and the other way around. When not set, MountPropagationNone is used. This field is beta in 1.10.
* **name**: string (Required): This must match the Name of a Volume.
* **readOnly**: bool: Mounted read-only if true, read-write otherwise (false or unspecified). Defaults to false.
* **subPath**: string: Path within the volume from which the container's volume should be mounted. Defaults to "" (volume's root).
* **subPathExpr**: string: Expanded path within the volume from which the container's volume should be mounted. Behaves similarly to SubPath but environment variable references $(VAR_NAME) are expanded using the container's environment. Defaults to "" (volume's root). SubPathExpr and SubPath are mutually exclusive.

## IoK8SApiCoreV1VolumeProjection
### Properties
* **configMap**: [IoK8SApiCoreV1ConfigMapProjection](#iok8sapicorev1configmapprojection): Adapts a ConfigMap into a projected volume.

The contents of the target ConfigMap's Data field will be presented in a projected volume as files using the keys in the Data field as the file names, unless the items element is populated with specific mappings of keys to paths. Note that this is identical to a configmap volume source without the default mode.
* **downwardAPI**: [IoK8SApiCoreV1DownwardAPIProjection](#iok8sapicorev1downwardapiprojection): Represents downward API info for projecting into a projected volume. Note that this is identical to a downwardAPI volume source without the default mode.
* **secret**: [IoK8SApiCoreV1SecretProjection](#iok8sapicorev1secretprojection): Adapts a secret into a projected volume.

The contents of the target Secret's Data field will be presented in a projected volume as files using the keys in the Data field as the file names. Note that this is identical to a secret volume source without the default mode.
* **serviceAccountToken**: [IoK8SApiCoreV1ServiceAccountTokenProjection](#iok8sapicorev1serviceaccounttokenprojection): ServiceAccountTokenProjection represents a projected service account token volume. This projection can be used to insert a service account token into the pods runtime filesystem for use against APIs (Kubernetes API Server or otherwise).

## IoK8SApiCoreV1VsphereVirtualDiskVolumeSource
### Properties
* **fsType**: string: fsType is filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
* **storagePolicyID**: string: storagePolicyID is the storage Policy Based Management (SPBM) profile ID associated with the StoragePolicyName.
* **storagePolicyName**: string: storagePolicyName is the storage Policy Based Management (SPBM) profile name.
* **volumePath**: string (Required): volumePath is the path that identifies vSphere volume vmdk

## IoK8SApiCoreV1WeightedPodAffinityTerm
### Properties
* **podAffinityTerm**: [IoK8SApiCoreV1PodAffinityTerm](#iok8sapicorev1podaffinityterm) (Required): Defines a set of pods (namely those matching the labelSelector relative to the given namespace(s)) that this pod should be co-located (affinity) or not co-located (anti-affinity) with, where co-located is defined as running on a node whose value of the label with key <topologyKey> matches that of any node on which a pod of the set of pods is running
* **weight**: int (Required): weight associated with matching the corresponding podAffinityTerm, in the range 1-100.

## IoK8SApiCoreV1WindowsSecurityContextOptions
### Properties
* **gmsaCredentialSpec**: string: GMSACredentialSpec is where the GMSA admission webhook (https://github.com/kubernetes-sigs/windows-gmsa) inlines the contents of the GMSA credential spec named by the GMSACredentialSpecName field.
* **gmsaCredentialSpecName**: string: GMSACredentialSpecName is the name of the GMSA credential spec to use.
* **hostProcess**: bool: HostProcess determines if a container should be run as a 'Host Process' container. This field is alpha-level and will only be honored by components that enable the WindowsHostProcessContainers feature flag. Setting this field without the feature flag will result in errors when validating the Pod. All of a Pod's containers must have the same effective HostProcess value (it is not allowed to have a mix of HostProcess containers and non-HostProcess containers).  In addition, if HostProcess is true then HostNetwork must also be set to true.
* **runAsUserName**: string: The UserName in Windows to run the entrypoint of the container process. Defaults to the user specified in image metadata if unspecified. May also be set in PodSecurityContext. If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.

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

## IoK8SApimachineryPkgApisMetaV1ManagedFieldsEntry
### Properties
* **apiVersion**: string: APIVersion defines the version of this resource that this field set applies to. The format is "group/version" just like the top-level APIVersion field. It is necessary to track the version of a field set because it cannot be automatically converted.
* **fieldsType**: string: FieldsType is the discriminator for the different fields format and version. There is currently only one possible value: "FieldsV1"
* **fieldsV1**: any: Any object
* **manager**: string: Manager is an identifier of the workflow managing these fields.
* **operation**: string: Operation is the type of operation which lead to this ManagedFieldsEntry being created. The only valid values for this field are 'Apply' and 'Update'.
* **subresource**: string: Subresource is the name of the subresource used to update that object, or empty string if the object was updated through the main resource. The value of this field is used to distinguish between managers, even if they share the same name. For example, a status update will be distinct from a regular update using the same manager name. Note that the APIVersion field is not related to the Subresource field and it always corresponds to the version of the main resource.
* **time**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.

## IoK8SApimachineryPkgApisMetaV1ObjectMeta
### Properties
* **annotations**: [IoK8SApimachineryPkgApisMetaV1ObjectMetaAnnotations](#iok8sapimachinerypkgapismetav1objectmetaannotations): Annotations is an unstructured key value map stored with a resource that may be set by external tools to store and retrieve arbitrary metadata. They are not queryable and should be preserved when modifying objects. More info: http://kubernetes.io/docs/user-guide/annotations
* **creationTimestamp**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **deletionGracePeriodSeconds**: int: Number of seconds allowed for this object to gracefully terminate before it will be removed from the system. Only set when deletionTimestamp is also set. May only be shortened. Read-only.
* **deletionTimestamp**: string: Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.
* **finalizers**: string[]: Must be empty before the object is deleted from the registry. Each entry is an identifier for the responsible component that will remove the entry from the list. If the deletionTimestamp of the object is non-nil, entries in this list can only be removed. Finalizers may be processed and removed in any order.  Order is NOT enforced because it introduces significant risk of stuck finalizers. finalizers is a shared field, any actor with permission can reorder it. If the finalizer list is processed in order, then this can lead to a situation in which the component responsible for the first finalizer in the list is waiting for a signal (field value, external system, or other) produced by a component responsible for a finalizer later in the list, resulting in a deadlock. Without enforced ordering finalizers are free to order amongst themselves and are not vulnerable to ordering changes in the list.
* **generateName**: string: GenerateName is an optional prefix, used by the server, to generate a unique name ONLY IF the Name field has not been provided. If this field is used, the name returned to the client will be different than the name passed. This value will also be combined with a unique suffix. The provided value has the same validation rules as the Name field, and may be truncated by the length of the suffix required to make the value unique on the server.

If this field is specified and the generated name exists, the server will return a 409.

Applied only if Name is not specified. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#idempotency
* **generation**: int: A sequence number representing a specific generation of the desired state. Populated by the system. Read-only.
* **labels**: [IoK8SApimachineryPkgApisMetaV1ObjectMetaLabels](#iok8sapimachinerypkgapismetav1objectmetalabels): Map of string keys and values that can be used to organize and categorize (scope and select) objects. May match selectors of replication controllers and services. More info: http://kubernetes.io/docs/user-guide/labels
* **managedFields**: [IoK8SApimachineryPkgApisMetaV1ManagedFieldsEntry](#iok8sapimachinerypkgapismetav1managedfieldsentry)[]: ManagedFields maps workflow-id and version to the set of fields that are managed by that workflow. This is mostly for internal housekeeping, and users typically shouldn't need to set or understand this field. A workflow can be the user's name, a controller's name, or the name of a specific apply path like "ci-cd". The set of fields is always in the version that the workflow used when modifying the object.
* **name**: string: Name must be unique within a namespace. Is required when creating resources, although some resources may allow a client to request the generation of an appropriate name automatically. Name is primarily intended for creation idempotence and configuration definition. Cannot be updated. More info: http://kubernetes.io/docs/user-guide/identifiers#names
* **namespace**: string: Namespace defines the space within which each name must be unique. An empty namespace is equivalent to the "default" namespace, but "default" is the canonical representation. Not all objects are required to be scoped to a namespace - the value of this field for those objects will be empty.

Must be a DNS_LABEL. Cannot be updated. More info: http://kubernetes.io/docs/user-guide/namespaces
* **ownerReferences**: [IoK8SApimachineryPkgApisMetaV1OwnerReference](#iok8sapimachinerypkgapismetav1ownerreference)[]: List of objects depended by this object. If ALL objects in the list have been deleted, this object will be garbage collected. If this object is managed by a controller, then an entry in this list will point to this controller, with the controller field set to true. There cannot be more than one managing controller.
* **resourceVersion**: string: An opaque value that represents the internal version of this object that can be used by clients to determine when objects have changed. May be used for optimistic concurrency, change detection, and the watch operation on a resource or set of resources. Clients must treat these values as opaque and passed unmodified back to the server. They may only be valid for a particular resource or set of resources.

Populated by the system. Read-only. Value must be treated as opaque by clients and . More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#concurrency-control-and-consistency
* **selfLink**: string: Deprecated: selfLink is a legacy read-only field that is no longer populated by the system.
* **uid**: string: UID is the unique in time and space value for this object. It is typically generated by the server on successful creation of a resource and is not allowed to change on PUT operations.

Populated by the system. Read-only. More info: http://kubernetes.io/docs/user-guide/identifiers#uids

## IoK8SApimachineryPkgApisMetaV1ObjectMetaAnnotations
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApimachineryPkgApisMetaV1ObjectMetaLabels
### Properties
### Additional Properties
* **Additional Properties Type**: string

## IoK8SApimachineryPkgApisMetaV1OwnerReference
### Properties
* **apiVersion**: string (Required): API version of the referent.
* **blockOwnerDeletion**: bool: If true, AND if the owner has the "foregroundDeletion" finalizer, then the owner cannot be deleted from the key-value store until this reference is removed. See https://kubernetes.io/docs/concepts/architecture/garbage-collection/#foreground-deletion for how the garbage collector interacts with this field and enforces the foreground deletion. Defaults to false. To set this field, a user needs "delete" permission of the owner, otherwise 422 (Unprocessable Entity) will be returned.
* **controller**: bool: If true, this reference points to the managing controller.
* **kind**: string (Required): Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds
* **name**: string (Required): Name of the referent. More info: http://kubernetes.io/docs/user-guide/identifiers#names
* **uid**: string (Required): UID of the referent. More info: http://kubernetes.io/docs/user-guide/identifiers#uids

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

