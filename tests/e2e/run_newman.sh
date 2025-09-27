#!/bin/bash
set -e

if ! command -v newman &> /dev/null; then
  echo "Newman غير مثبت. استخدم 'npm install -g newman' لتثبيته." >&2
  exit 1
fi

BASE_URL=${BASE_URL:-http://localhost:8080/api/v1}

newman run ../../docs/postman_collection.json \
  --env-var baseUrl=$BASE_URL \
  --reporters cli --disable-unicode