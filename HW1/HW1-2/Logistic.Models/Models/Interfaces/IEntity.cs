﻿namespace Logistic.Models.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public List<Cargo>? Cargoes { get; set; }
    }
}