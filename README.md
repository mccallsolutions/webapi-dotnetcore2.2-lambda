# Web API with dotnetcore2.2 and deploying to AWS using Serverless

The aim of this project was to get a .NET core 2.2 web API Lambda running in AWS and have it deployed using Serverless from a Mac.

At the moment (July 2019) AWS only supports .NET core 2.1 so I had to use a custom runtime as described [here](https://aws.amazon.com/blogs/developer/announcing-amazon-lambda-runtimesupport/).

There are 2 problems I'm facing:

## Problem One

Deploying with Serverless (`serverless.yml`) by setting the `runtime` to `dotnetcore2.2` fails due to:

```
CloudFormation - CREATE_FAILED - AWS::Lambda::Function - ApiLambdaFunction

An error occurred: ApiLambdaFunction - Value dotnetcore2.2 at 'runtime' failed to satisfy constraint: Member must satisfy enum value set: [java8, nodejs8.10, nodejs10.x, python2.7, python3.6, python3.7, dotnetcore2.1, go1.x, ruby2.5] or be a valid ARN (Service: AWSLambdaInternal; Status Code: 400; Error Code: InvalidParameterValueException; Request ID: 98602a5e-eefe-4265-92d5-bf22cd9c821c).
```

If I set the `runtime` to be `dotnetcore2.1` the stack deploys fine but then I face problem two.

## Problem Two

Testing the deployed API in AWS the logs show this error:

```
It was not possible to find any compatible framework version
The specified framework 'Microsoft.AspNetCore.App', version '2.2.0' was not found.
- Check application dependencies and target a framework version installed at:
/var/lang/bin/
- Installing .NET Core prerequisites might help resolve this problem:
https://go.microsoft.com/fwlink/?LinkID=798306&clcid=0x409
- The .NET Core framework and SDK can be installed from:
https://aka.ms/dotnet-download
- The following versions are installed:
2.1.11 at [/var/lang/bin/shared/Microsoft.AspNetCore.App]
START RequestId: 0d13fb1f-b89b-42a6-8ce7-afa7038fb834 Version: $LATEST
Failed to execute the Lambda function. The dotnet CLI failed to start with the provided deployment package. Please check CloudWatch logs for this Lambda function to get detailed information about this failure.: LambdaException
```

This is understandable as the `runtime` in Serverless is set to `dotnetcore2.1`, but how can I deploy the stack as Serverless won't allow me to deploy due to problem one?

I think I've coded this correctly and this is a Serverless limitation. My next options are to use AWS SAM or Docker maybe, but I'd like to get custom runtimes working!
