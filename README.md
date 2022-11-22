# SNAT port exhaustion in AKS - how to diagnose and mitigate

## Background

Network Address Translation (NAT) is used widely across the internet and is commonly used as a means of allowing a large number of devices on a private network to make requests to the internet using a common outbound IP address. Your home network performs NAT so that any requests from one of your devices in your home network can be made to the internet and the responses routed back the correct device. In the case of a home network, the router provides the NAT capability so that to the outside world all devices in your home network appear as a single outbound IP address. See this Wikipedia article here https://en.wikipedia.org/wiki/Network_address_translation

When NAT is used with networking, there has to be a means of mapping outbound and inbound ports. The service performing NAT has to keep hold of the return address of the network endpoint behind the NAT gateway. At an infrastructure level TCP IPv4 only has 64k ports, which seems plenty, but shared platform services often have to share this across 64k ports across a number of workloads.

In the case of Azure Kubernetes Service (AKS), NAT is slightly different depending on whether the AKS cluster is configured for kubnet or Azure CNI. See here https://learn.microsoft.com/en-us/azure/aks/concepts-network. But for workloads inside AKS that need to access resources outside of the virtual network environment (VNet), NAT is always used and is essentially managed by an Azure load balancer that is provisioned when the AKS cluster gets created.

This load balancer by default only hands out 1000 TCP ports per node pool VM instance. This 1000 also gets multiplied by the number of public IP addresses attached to the load balancer.

Any applications deployed to an AKS cluster therefore will have a limited set NAT ports when calling services outside its VNet. How the application code is written and how quickly any dependent calls outside of the cluster respond, will have a major impact on the usage of these NAT ports. 

## Sample Application

![alt text](/images/overview.png "Sample app overview")

1. Async
2. Reuse of connections
3. The amount of data outbound from the AKS-hosted app to the dependency and how much data is returned from the dependency
4. Generally how long a dependent call takes (as it is keeping the underlying TCP connection open)

What can go wrong

## How to test

## How to monitor and diagnose

## How to mitigate/remediate

# Conclusions
