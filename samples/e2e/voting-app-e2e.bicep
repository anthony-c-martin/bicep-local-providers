extension utils

resource getKubeConfig 'Script' = {
  type: 'bash'
  script: 'cat ~/.kube/config'
}

module k8s '../kubernetes/voting-app.bicep' = {
  name: 'voting-app'
  params: {
    kubeConfig: base64(getKubeConfig.stdout)
  }
}
