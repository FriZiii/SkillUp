#!/usr/bin/env bash
awslocal s3 mb s3://skillup-s3-dev
awslocal s3api put-bucket-cors --bucket skillup-s3-dev --cors-configuration file:///etc/localstack/init/ready.d/cors-config.json