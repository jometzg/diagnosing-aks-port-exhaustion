# SNAT port exhaustion in AKS - how to diagnose and mitigate

## Background

When NAT is used with networking, there has to be a means of mapping outbound and inbound ports. The NATing service has to keep hold of the return address of the network endpoint behind the NAT gateway. At an infrastructure level TCP IPv4 only has 64k ports, which seems plenty, but shared platform services often have to share this across a number of workloads.

In the case of AKS, a load balancer is provisioned in a cluster, but this load balancer by default, only hands out 1000 ports per node pool VM instance. This 1000 also gets multiplied by the number of public IP addresses attached to the load balancer.

Any applications deployed to an AKS cluster therefore will have a limited set NAT ports when calling services outside its VNet. How the application code is written and how quickly any dependent calls outside of the cluster respond, will have a major impact on the usage of these 1000 per node pool instance NAT ports. 

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
