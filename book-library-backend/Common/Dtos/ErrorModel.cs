﻿namespace Domain.Dtos;

public class ErrorModel
{
    public string FieldName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}