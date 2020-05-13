﻿namespace BarcodeGenerator.Services
{
    public interface IBarcodeService
    {
        string GenerateNewBarcode(int code);
        string GenerateBarcode(string code);
        int CalcCheckDigit(string code);
    }
}