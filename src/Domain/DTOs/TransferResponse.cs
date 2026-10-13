namespace MiniBankLedger.Domain.DTOs;

public record class TransferResponse(string Source, string Destination, decimal Amount);
