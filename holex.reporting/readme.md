# Holiday Expense Report Generator

A Timer Triggered function which 
- reads the expenses stored in an Azure Storage Table
- calculates the totals since the last run
- sends a message with the totals on the service bus after the calculation is done
- the timer schedule is configurable with a default of 1 day

## What's implemented so far
1. A simple timer trigger with a fixed schedule to run every 5 seconds
2. Send a simple message to the service bus. 
   The service bus message containsw
	a. the current time 
	b. total which is simply a random number between 1 and 100