provider utils

resource getKubeConfig 'Script' = {
  type: 'bash'
  script: 'cat ~/.kube/config'
}

module k8s '../kubernetes/echo-server.bicep' = {
  name: 'echo-server'
  params: {
    kubeConfig: base64(getKubeConfig.stdout)
  }
}
