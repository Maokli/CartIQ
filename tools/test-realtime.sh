
#!/usr/bin/env bash

set -euo pipefail

API_URL="https://localhost:7278"
RECEIPT_FILE="$HOME/Downloads/receipt.jpg"

echo "=================================="
echo " CartIQ - Receipt Realtime Test"
echo "=================================="

# 1. Upload receipt
echo ""
echo "[1/2] Uploading receipt..."

RESPONSE=$(curl -sk -X POST \
    "$API_URL/api/Files/Upload" \
    -F "file=@$RECEIPT_FILE")

echo "Upload response:"
echo "$RESPONSE" | jq .

# 2. Extract receipt ID
RECEIPT_ID=$(echo "$RESPONSE" | jq -r '.resultItem')

if [[ -z "$RECEIPT_ID" || "$RECEIPT_ID" == "null" ]]; then
    echo "ERROR: Could not extract receipt ID."
    exit 1
fi

echo ""
echo "Receipt ID: $RECEIPT_ID"

# 3. Connect to SSE
echo ""
echo "[2/2] Connecting to receipt event stream..."
echo "Listening for events. Press Ctrl+C to stop."
echo ""

curl -k -N \
    -H "Accept: text/event-stream" \
    "$API_URL/api/realtime/receipts/$RECEIPT_ID"
