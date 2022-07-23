namespace GameExtension

open System
open Stride.Engine
open Stride.Core.Mathematics
open Stride.Physics

type MoveComponentScript(entity : Entity) =
    inherit SyncScript()

    let mutable rigidbody : RigidbodyComponent option = None
    let mutable force: float32 = 4000f

    override __.Start() =
        rigidbody <- entity.Get<RigidbodyComponent>() |> Some
        rigidbody.Value.Mass <- 1000f

    override __.Update() =
        match rigidbody with
        | Some b -> 
            if MathF.Round(b.LinearVelocity.Length(), 1) = 0f then
                b.ApplyImpulse(new Vector3(force, 0f, 0f))
                force <- force * -1f
        | None -> ()