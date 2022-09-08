﻿using Demo.Domain.PersonAggregate;

namespace Demo.Domain.CarAggregate;

public class Car
{
    private Car(CarId id)
    {
        Id = id;
    }

    private Car()
    {
        // Parameterless constructor for serialisation
    }

    // Private setter for serialisation
    public CarId Id { get; private set; }

    public Registration? Registration { get; private set; }

    public PersonId? Owner { get; private set; }

    public void ChangeOwner(PersonId owner)
    {
        Owner = owner;
    }

    public void Register(Registration registration)
    {
        Registration = registration;
    }

    public static Car CreateInstance(CarId id)
    {
        return new(id);
    }
}