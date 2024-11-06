using LibServices.Security.Cryptography.Factories.Messages;
using LibServices.Security.Cryptography.Factories.PublicKeys;
using LibServices.Security.Cryptography.Factories.RandomNumbers;

string curveName = "secp256k1";
string message = "Hello World!";

var privateKeyService = Factory_SecureRandomBigIntegerGenerator.Create(curveName);

Console.WriteLine("Private Key:");
Console.WriteLine("Hexadecimal:     " + privateKeyService.PrivateKeyHex);
/*
Console.WriteLine("Decimal:         " + privateKeyService.PrivateKeyDecimal);
Console.WriteLine("Octal:           " + privateKeyService.PrivateKeyOctal);
Console.WriteLine("Base64:          " + privateKeyService.PrivateKeyBase64);
Console.WriteLine("DER:             " + privateKeyService.PrivateKeyDER);
Console.WriteLine();
Console.WriteLine("PEM:\n" + privateKeyService.PrivateKeyPEM);
Console.WriteLine();
*/
var publicKeyService = Factory_PublicKeyGenerator.Create(curveName, privateKeyService.PrivateKey);

Console.WriteLine("Public Key:");
Console.WriteLine("Hexadecimal:     " + publicKeyService.PublicKeyHex);
Console.WriteLine("Compressed:      " + publicKeyService.PublicKeyCompressedHex);
Console.WriteLine("Uncompressed:    " + publicKeyService.PublicKeyUncompressedHex);
Console.WriteLine("Decimal:         " + publicKeyService.PublicKeyDecimal);
Console.WriteLine("Octal:           " + publicKeyService.PublicKeyOctal);
Console.WriteLine("Base64:          " + publicKeyService.PublicKeyBase64);
Console.WriteLine("DER:             " + publicKeyService.PublicKeyDER);
Console.WriteLine();
Console.WriteLine("Public Point:");
Console.WriteLine("{");
Console.WriteLine("    X:           " + publicKeyService.PublicKey_X.ToString("X"));
Console.WriteLine("    Y:           " + publicKeyService.PublicKey_Y.ToString("X"));
Console.WriteLine("}");
Console.WriteLine();
Console.WriteLine("PEM:\n" + publicKeyService.PublicKeyPEM);

Console.WriteLine();

var (R, s) = Factory_ECDsaSigner.CreateSigner(curveName, privateKeyService.PrivateKey, message);
Console.WriteLine("Message to Sign: " + message);
Console.WriteLine("Signature:");
Console.WriteLine("{");
Console.WriteLine("    R:           " + R.ToString("X"));
Console.WriteLine("    s:           " + s.ToString("X"));
Console.WriteLine("}");


var verifierService = Factory_ECDsaVerifier.CreateVerifier(curveName);
bool isSignatureValid = verifierService.VerifySignature(
    message,
    publicKeyService.PublicKey_X,
    publicKeyService.PublicKey_Y,
    R,
    s
    );

Console.WriteLine($"Signature validation: {isSignatureValid}");

Console.ReadKey();