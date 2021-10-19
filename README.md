![Continuous Integration](https://github.com/calinvlt/idempotent-api/actions/workflows/ci.yaml/badge.svg)

### Introduction

In a microservice architecture we no longer have transactions, specifically database ACID transactions. For example, if from an application we call 3 services, there is no automatic mechanism like a database transaction to guarantee all the operations were completed sucessfully or, in case of errors, all the operations have been rolledback so the data is consistent; basically there are no distributed transactions.

Not all the hope is lost, however we need to come up with our own mechanism, for example, retries. From our example if we call 3 services and let's say the second one fails, we detect the error and one of the options is we can retry again all the calls. 

But there is a problem, if the first call was successfull and we retry, we'll make another call to the same service resulting in the same operation being performed twice which in some cases it can be a bad thing. For example if the first call was a payment we end up with duplicate payments which is not what we want.

The solution is for the services to be idempotent, basicaly to allow duplicate calls.
