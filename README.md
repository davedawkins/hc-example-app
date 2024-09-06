# Notes

## View Functions

Functions named "viewXXXX" will render HTML with Sutil. In general they have the form:

```fs
    let view (input) (output) = ...
```

Some examples:

```fs
    let editThing (thing : Thing) (update: Thing -> unit) = ...
```

Allow the user to edit a thing, and give you back a valid thing

```fs
    let viewThing (thing : IStore<Thing>) (commandX: unit -> unit) (commandY: unit -> unit) = ...
```

View a thing that may change over time, and notify you if the user wants to do either X or Y

Note that it wouldn't make sense to have edit function such as:

```fs
    // Strange edit function
    let editThing (thing : IObservable<Thing>) (update: Thing -> unit) = ...
    // Strange edit function #2
    let editThing (thing : IStore<Thing>) (update: Thing -> unit) = ...
```

This would imply that what we expect what we're (trying to) edit to change while we're edit it.


## Sutil Operators

 Breaking this expression down:
   (s .>> _.Property)

   .>>        is equivalent to `Store.mapDistinct`
   _.Property is equivalent to `fun x -> x.Property`

 mapDistinct is useful when other state changes are being made, and we don't 
 want to propagate redraws for our particular projection. It will only "fire" 
 an update to the observable if the projection changes value.

 Other operators:
 .>         is equivalent to `Store.map`


