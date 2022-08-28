#Messeger
[![Build Status](https://travis-ci.org/yongwang0705/Messager.svg?branch=main)](https://travis-ci.org/yongwang0705/Messager)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/yongwang0705/Messager/pulls)
[![GitHub stars](https://img.shields.io/github/stars/yongwang0705/Messager.svg?style=social&label=Stars)](https://github.com/yongwang0705/Messager)
[![GitHub forks](https://img.shields.io/github/forks/yongwang0705/Messager.svg?style=social&label=Fork)](https://github.com/yongwang0705/Messager)

## Introduction
A C/S windows desktop application which aims to offer the complete solution on internal communication of enterprise. It’s built on .Net FrameWork 4.7.2 and Asure.  It supports registration, user identity verification and messages distribution etc.
## Functions Flowchat
#### <center>SignUp
```mermaid
graph TB;
subgraph Client
CFS(FrmSignUp.cs)
CC(Client.cs)
end

CFS--Send-->CC
CC--Read-->CFS

subgraph Server
SS[Server.cs]
D(DataBusiness.cs)
U(UIOperator.cs)
end

SS-->U--UpdateLog-->Log.cs
CC--SignUp Request-->SS
SS--Id-->CC
SS--CreateUser-->D
D--Insert user-->DB[(Sql Server)]

```
#### <center>SignIn
```mermaid
graph TB;
subgraph Client
CFS(FrmSignIn.cs)
CC(Client.cs)
CU(UIOperator.cs)
CFL(FrmUserList.cs)
end

CFS--Send-->CC
CC--Read-->CFS
CC-->CU
CU--Update-->CFL

subgraph Server
SS[Server.cs]
D(DataBusiness.cs)
SU(UIOperator.cs)
end

CC--SignIn Request-->SS
SS--Id-->CC
SS--VerifyUser-->D
D--Select user-->DB[(Sql Server)]
SS-->SU--UpdateLog-->Log.cs
```
#### <center>Message Send
```mermaid
graph TB;
subgraph ClientOne
CFS(FrmChat.cs)
CC(Client.cs)
end

CFS--Send to ClientTwo-->CC

subgraph Server
SS[Server.cs]
D(DataBusiness.cs)
end

CC--Send Request-->SS

SS--Save message-->D
D--Insert message-->DB[(Sql Server)]

subgraph ClientTwo
CFS2(FrmChat.cs)
CC2(Client.cs)
CU2(UIOperator.cs)
end

SS--Distribute message-->CC2
CC2-->CU2
CU2--Update-->CFS2

```

## Architechture
```mermaid
graph TB;
MoninorThread(FrmChat.cs)

````