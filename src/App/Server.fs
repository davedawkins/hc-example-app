module Server

open Fable
open Fable.Core
open Types

type Promise<'t> = JS.Promise<'t>

module ExampleData =
    // https://www.briandunning.com/sample-data/
    let users : User[] =
        [|
            { Name = "James Butt"; Username = "jbutt"; Email = "jbutt@gmail.com" }   
            { Name = "Josephine Darakjy"; Username = "josephine_darakjy"; Email = "josephine_darakjy@darakjy.org" }   
            { Name = "Art Venere"; Username = "art"; Email = "art@venere.org" }   
            { Name = "Lenna Paprocki"; Username = "lpaprocki"; Email = "lpaprocki@hotmail.com" }   
            { Name = "Donette Foller"; Username = "donette"; Email = "donette.foller@cox.net" }   
        |]

///
/// Server is our database connection, and will cache static data for us. 
/// 
type Server() = 

    let mutable users = ExampleData.users

    static member Create() : Promise<Server> =
        promise {
            do! Promise.sleep(500)

            return new Server()
        }

    member __.Users() : Promise<User[]> =
        promise { return users }
