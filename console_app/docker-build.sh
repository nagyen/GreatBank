#!/bin/bash
#publish console
sh ./publish-console.sh &&
#build dockerfile
docker build -t nyendluri/greatbank-console:1.0 .