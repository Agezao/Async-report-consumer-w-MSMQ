# Async report consumer w/ MSMQ
Async consumer to generate reports and send them by email using MSMQ as queuing service

## Setting up
Make sure to have MSMQ installed. To do so, reach to windows resources and add MSMQ:
<img src="https://raw.githubusercontent.com/Agezao/Async-report-consumer-w-MSMQ/master/ActivateMsmq.PNG" width="200" />

After installing, to check your Queues go to Computer Management and you'll be able to check your queues
<img src="https://raw.githubusercontent.com/Agezao/Async-report-consumer-w-MSMQ/master/Checking%20queues.PNG" width="300" />

## What this do?
[x] Works as a windows console app
[x] Check periodically MSMQ for new entries
[x] Process message request
[x] Connect to PostgreSQL to fetch some mockdata
[x] Parse a report viewer XLS
[x] Send XLS via email using SMTP

The end result is something like this:
<img src="https://raw.githubusercontent.com/Agezao/Async-report-consumer-w-MSMQ/master/SentMail.PNG" width="300" />