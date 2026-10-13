namespace MiniBankLedger.Domain.DTOs;

public record class TransferRequest(string Source, string Destination, string Amount);
