# dotnet-element-admin

> A Vue.js project in dotnet

## Usage
### Install Template
```bash
dotnet new -i DotnetElementAdmin::1.0.2
```

### Uninstall Template
```bash
dotnet new -u DotnetElementAdmin
```

### Create New Project
```bash
# Create an empty vue project
dotnet new element-admin -o <project name>
```

### Run Project
```bash
dotnet run
```

### If developing, you need build vue view 
```bash
yarn run build
```

### Publish The Project
```bash
dotnet publish -c Release -p:PublishTrimmed=true -p:PublishSingleFile=true --self-contained true -r linux-x64 -o out

dotnet publish -c Release -p:PublishTrimmed=true -p:PublishSingleFile=true --self-contained true -r osx-x64 -o out
```