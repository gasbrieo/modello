﻿namespace Modello.Domain.Common.Exceptions;

public abstract class NotFoundException(string message) : Exception(message);
