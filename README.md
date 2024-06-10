# Local-only Bicep
This document explains how to set up the experimental local-only deployment support for 3rd party extensibility providers, without a dependency on Azure.

Here's an example of deploying to a local kubernetes cluster, logging, and executing a bash script via terminal:

<img width="815" alt="image" src="https://github.com/anthony-c-martin/bicep/assets/38542602/7e0c353f-7d9b-4fd0-9468-bf877680a3e0">

Here's an example of using VSCode to dpeloy to a local kubernetes cluster:

https://github.com/anthony-c-martin/bicep/assets/38542602/b9450f54-7272-418b-8c5a-9c62f122d2b4

## Installing
### Mac/Linux
```sh
# install the CLI to ~/.azure/bin/bicep
bash <(curl -Ls https://aka.ms/bicep/nightly-cli.sh)
# install the VSCode Extension
bash <(curl -Ls https://aka.ms/bicep/nightly-vsix.sh)
```

### Windows
```sh
# install the CLI to ~/.azure/bin/bicep
iex "& { $(irm https://aka.ms/bicep/nightly-cli.ps1) }"
# install the VSCode Extension
iex "& { $(irm https://aka.ms/bicep/nightly-vsix.ps1) }"
```

## Running Samples
* Copy the full [samples](./samples) folder locally. You can use [this tool](https://download-directory.github.io/?url=https%3A%2F%2Fgithub.com%2Fanthony-c-martin%2Fbicep-local-providers%2Ftree%2Fmain%2Fsamples) to download it as a zip file.
* For testing with the Kubernetes provider, you will need access to a cluster configured in your kubeconfig file. If you have Docker installed, this can be obtained by [Enabling Kubernetes Support](https://docs.docker.com/desktop/kubernetes/).

### Via CLI
Replace `<path_to_bicepparam>` with the path to the `.bicepparam` file you wish to deploy.
```sh
~/.azure/bin/bicep local-deploy <path_to_bicepparam>
```

### Via VSCode
Open the [Deployment Pane](https://github.com/Azure/bicep/blob/main/docs/experimental/deploy-ui.md) for a `.bicepparam` file you wish to deploy, and press the "Deploy" button.

## Utils Samples
### Bash/PowerShell script execution ([samples/utils/script.bicepparam](./samples/utils/script.bicepparam))
Execute a bash or powershell script as part of a local deployment.

### "Wait" functionality ([samples/utils/wait.bicepparam](./samples/utils/wait.bicepparam))
Introduce a sleep for a given number of milliseconds in your deployment.

### Logging functionality ([samples/utils/log.bicepparam](./samples/utils/log.bicepparam))
Write a log to console during your deployment to help debug.

### Assertion functionality ([samples/utils/assert.bicepparam](./samples/utils/assert.bicepparam))
Fail the deployment if a certain condition is false.

## Kubernetes Samples
### Voting App ([samples/kubernetes/voting-app.bicepparam](./samples/kubernetes/voting-app.bicepparam))

This will run the [Voting App Sample](https://github.com/Azure-Samples/azure-voting-app-redis) locally.

After deploying, test it out by navigating to [http://localhost](http://localhost) in a browser.

Cleanup:
```sh
kubectl delete deployment azure-vote-back
kubectl delete deployment azure-vote-front
kubectl delete service azure-vote-back
kubectl delete service azure-vote-front
```

### Simple web server ([samples/kubernetes/echo-server.bicepparam](./samples/kubernetes/echo-server.bicepparam))

This will run the [echo-server](https://ealenn.github.io/Echo-Server/) service locally.

Test it out by submitting a request:
```sh
curl -I localhost:8080
```

Cleanup:
```sh
kubectl delete deployment echo-server 
kubectl delete service echo-server
```

## GitHub Samples
### Fetch Repo information ([samples/github/repo.bicepparam](./samples/github/repo.bicepparam))

Fetches a repo + contributor from GitHub.

## Contributing new providers or types
I'm happy to take contributions to this repo to extend experimental providers or add new ones. Please reach out to me via the issue tracker for more information.

## Caveats
* There is currently no support for deploying Azure resources. Theoretically there's no reason why this can't work, I just haven't had the time to build it.
* Provider binary packages are not currently signed. If you see the following error on Mac, you may need to manually sign the provider package:
    > Failed to launch provider: Failed to connect to provider /Users/ant/.bicep/br/biceplocaldeploy.azurecr.io/providers$utils/0.1.1$/provider.bin
   
   To work around it, run the following in a terminal window, using the path from the error message:
   ```sh
   codesign -s - '/Users/ant/.bicep/br/biceplocaldeploy.azurecr.io/providers$utils/0.1.1$/provider.bin'
   ```