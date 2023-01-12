# Simplz.Loki.Relay

Code for AWS Lamda function, requires SNS Topic if you need the email content

```python
import json
import urllib.request

def lambda_handler(event, context):
    url = "https://demo"
    message = event['Records'][0]['Sns']['Message']
    data = message.encode('utf8')
    req = urllib.request.Request(url, data=data, headers={'content-type': 'application/json'})
    response = urllib.request.urlopen(req)
    return response.read()
```
