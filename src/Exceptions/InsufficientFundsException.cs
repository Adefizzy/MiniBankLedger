using System;

namespace MiniBankLedger.Exceptions;

public class InsufficientFundsException(string Message): Exception(message: Message);

