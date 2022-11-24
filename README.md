# Application State

This is a sample that shows the use of [Stateless]() and [MediatR]() to model mobile application foregrounding, background, background launch, connecting, and disconnecting.

The goal of this sample is to solve the following requirements

- [ ] Provide a State Machine that models the application state
- [ ] Allow the State Machine to produce state information for down stream subscribers
- [ ] The solution should allow certain routines to execute during certain state transitions
  - [ ] The routines should execute before the notification is sent to the downstream subscribers
- [ ] Keep the concerns as clear and separate as possible.

## Patterns

- Mediator Design Pattern
  - Isolate routines in Handlers
  - Ensure the correct handler executes for the given event
  - Allows a set of routines to execute based on the message received

- State Design Pattern
  - Encapsulate Application State
  - Encapsulate handling of changes to Application State
  - Allow observers to see changes in state over time

# Acknowledgments
- [MediatR](https://github.com/jbogard/MediatR)
- [Stateless](https://github.com/dotnet-state-machine/stateless)