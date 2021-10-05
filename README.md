## CURRENT TASK TO DO
Quiz
Moneybox continue
Pnl changes done:
- Removed static service - now a part of StrategyList
- Anything csv related is in StrategyReader
- No constructor for StrategyList

Right now it is: Program -> StrategyList -> StrategyReader
Should be able to test by functionality by using methods in StrategyReader (pass it dummy data defined in tests).

## OLD
you are passed a list of IDs and an object containing a string 'event' (the state) and the corresponding ID.
write tests to figure out which state the ID is on and/or which events is has passed.
events are:
- non existent
- create
- in progress/actions
- completed
- error
use linq as much as possible
event's an ordered list