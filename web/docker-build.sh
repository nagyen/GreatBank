#!/bin/bash
#publish web
sh ./publish-web.sh
#build dockerfile
docker build -t nyendluri/greatbank-web:1.0 .