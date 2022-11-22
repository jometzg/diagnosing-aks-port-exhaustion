# SNAT port exhaustion in AKS - how to diagnose and mitigate

## Background

Network Address Translation (NAT) is used widely across the internet and is commonly used as a means of allowing a large number of devices on a private network to make requests to the internet using a common outbound IP address. Your home network performs NAT so that any requests from one of your devices in your home network can be made to the internet and the responses routed back the correct device. In the case of a home network, the router provides the NAT capability so that to the outside world all devices in your home network appear as a single outbound IP address. See this Wikipedia article here https://en.wikipedia.org/wiki/Network_address_translation

When NAT is used with networking, there has to be a means of mapping outbound and inbound ports. The service performing NAT has to keep hold of the return address of the network endpoint behind the NAT gateway. At an infrastructure level TCP IPv4 only has 64k ports, which seems plenty, but shared platform services often have to share this across 64k ports across a number of workloads.

In the case of Azure Kubernetes Service (AKS), NAT is slightly different depending on whether the AKS cluster is configured for kubnet or Azure CNI. See here https://learn.microsoft.com/en-us/azure/aks/concepts-network. But for workloads inside AKS that need to access resources outside of the virtual network environment (VNet), NAT is always used and is essentially managed by an Azure load balancer that is provisioned when the AKS cluster gets created.

This load balancer by default only hands out 1000 TCP ports per node pool VM instance. This 1000 also gets multiplied by the number of public IP addresses attached to the load balancer.

Any applications deployed to an AKS cluster therefore will have a limited set NAT ports when calling services outside its VNet. How the application code is written and how quickly any dependent calls outside of the cluster respond, will have a major impact on the usage of these NAT ports.

When there are no NAT ports left to service outbound connections, errors will occur as connections will no longer be made correctly. This is what is referred to as "NAT port exhaustion".

## Sample Application

![alt text](/images/overview.png "Sample app overview")

In order to demonstrate how NAT port exhaustion can occur, several thinga are needed:
1. An application (web API) hosted in AKS that makes HTTP requests to a remote service - preferably outside the AKS VNet. It is also useful to be able to vary the target and to re-use or not HTTP connections to see how this impacts its behaviour. In this repository, this is referred to as the "source"
2. A target application (web API) outside of AKS and preferably outside of the VNet that accepts requests from the source and returns responses. Some means of varying the response time from this service is also useful. In this repository, this is referred to as the "target".
3. A means of driving load into the source in a reliable manner. A JMeter load test is used and driven by Azure load test https://learn.microsoft.com/en-gb/azure/load-testing/overview-what-is-azure-load-testing
4. Monitoring AKS and the API in AKS.


6. Async
7. Reuse of connections
8. The amount of data outbound from the AKS-hosted app to the dependency and how much data is returned from the dependency
9. Generally how long a dependent call takes (as it is keeping the underlying TCP connection open)

What can go wrong

## How to test

## How to monitor and diagnose

## How to mitigate/remediate

![alt text](https://learn.microsoft.com/en-us/azure/load-balancer/media/load-balancer-outbound-connections/outbound-options.png "NAT in Azure")

# Conclusions
