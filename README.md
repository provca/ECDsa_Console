# **ECDSA Digital Signature Verification Library**

## **Presentation**
ECDSA_Console implements a digital signature verification library based on ECDSA (Elliptic Curve Digital Signature Algorithm), a cryptographic technique that ensures the integrity and authenticity of messages through the use of elliptic curves. The library is written in C# and is structured into modular classes and components to facilitate integration and use in various applications that require secure cryptography.

## **Project Description**
This implementation provides the capability to generate and verify digital signatures while superficially adhering to the ANSI X9.62 elliptic curve cryptography standards. The system includes secure random number generation, robust curve parameter verification, and components that allow for a complete message signing and verification flow. It uses specialized classes to represent and manipulate points on the curve, generate public/private keys, and verify digital signatures.

The library is designed to work with different representations of public and private keys, including formats such as PEM, DER, Base64, Hexadecimal, and Octal. Additionally, it offers compatibility with elliptic curves of various configurations, focusing on flexibility and performance.

## **Features**
- **Public Key Generation**: Derives the public key using the private key and the elliptic curve base point.
- **ECDSA Signing and Verification**: Signs messages and verifies authenticity using curve point coordinates.
- **Curve Parameter Validation**: Ensures that curve parameters are valid and secure before use.
- **Secure Random Number Generation**: Uses a DRBG (Deterministic Random Bit Generator) based random number generator.
- **Support for Multiple Curves**: Currently supports standard curves like secp256k1, with flexibility to extend to other curves.
- **Secure Message Hashing**: Computes a secure and unique hash of the messages before signing, using SHA-1 for consistency with standards.

## **Use Cases and Applications**
- **Message Authentication**: Verifying messages between clients and servers in secure environments.
- **Data Integrity**: Used in applications that require ensuring that data has not been tampered with.
- **Legal Digital Signatures**: Ideal for authentication systems where digital signatures hold legal validity.

## **Compatibility**
This project is compatible with:
- .NET Core 3.1 and above
- .NET 5/6/7
- C# 8.0 and above
- Platforms: Windows, Linux, and macOS.

## **Technical Details**
This project is designed to follow a modular structure, with specialized classes for each component of ECDSA:
- **Key Representations**: Supports multiple formats for keys, including PEM, DER, Base64, Hexadecimal, and Octal.
- **Elliptic Curves**: The library is compatible with various elliptic curves and can be extended to include new curves as needed. The current implementation is based on a custom curve.
- **Signature Generation and Verification**: `ECDsaSigner` and `ECDsaVerifier` are responsible for creating signatures and validating them. ECDSA signing uses a securely generated random number `k`.
- **Curve Validation**: The `CurveVerifier` class ensures that curve parameters meet non-singularity and other field-specific checks. This includes checks for `a`, `b`, the base point `G`, and the order `n`.
- **Random Number Generation**: `SecureRandomBigIntegerGenerator` creates high-entropy random numbers for the `k` value in the ECDSA signing process.
- **Verification Factory**: `Factory_ECDsaVerifier` enables quick creation of verification instances with specific configurations for different types of curves.

### File Structure
The Repository structure is organized as follows:
```
ECDsa_Console
│
├── Program.cs
│
LibServices
│
├── LibServices
│    │
│    └── Security
│        │
│        └── Cryptography
│            │
│            └── Factories
│                │
│                ├── RandomNumbers
│                │   └── Factory_SecureRandomBigIntegerGenerator.cs
│                │
│                ├── PublicKeys
│                │   └── Factory_PublicKeyGenerator.cs
│                │
│                ├── Messages
│                │   ├── Factory_ECDsaVerifier.cs
│                │   └── Factory_ECDsaSigner.cs
│                │
│                │
│                └── ECDsaService
│                    │
│                    ├── Service_CurveParameters.cs
│                    │
│                    ├── RandomNumbers
│                    │   └── Service_SecureRandomBigIntegerGeneratorForCurve.cs
│                    │
│                    ├── PublicKeys
│                    │   └── Service_GeneratePublicKeysFromCurve.cs
│                    │
│                    └── Messages
│                        ├── Service_ECDsaVerifier.cs
│                        └── Service_ECDsaSigner.cs
│
LibECDsa
│
└── LibECDsa
    │
    ├── ECPoint.cs
    │
    ├── Curves
    │   └── SecP256K1Parameters.cs
    │
    ├── Enums
    │   ├── CurveBitSize.cs
    │   └── CurveType.cs
    │
    ├── Interfaces
    │   └── ICurveParameters.cs
    │
    ├── Operations
    │   ├── Messages
    │   │   ├── ECDsaSigner.cs
    │   │   └── ECDsaVerifier.cs
    │   │
    │   ├── PublicKeys
    │   │   ├── PublicKeyRepresentation.cs
    │   │   └── PublicKeySerializer.cs
    │   │
    │   └── Utilities
    │       ├── BitsFlow.cs
    │       ├── CurveHasher.cs
    │       ├── CurveVerifier.cs
    │       ├── NumeralSystem.cs
    │       └── SecureRandomBigIntegerGenerator.cs
    │
    └── EllipticCurve.cs
```

## **Scalability**
To add a new curve class, follow these steps:
1. Add the curve class in `LibECDsa\Curves\`.
2. Maintain the naming convention: `NameCurveParameters.cs`.
2.1. Implement the `ICurveParameters` interface.
4. Register the curve in `LibECDsa\Enums\CurveType.cs`.
5. Add the curve bits in `LibECDsa\Enums\CurveBitSize.cs` if they don't already exist.
6. Register the `NameCurveParameters.cs` class in `LibServices.Security.Cryptography.ECDsaService\Service_CurveParameters.cs`.

## **Implementation**
The `ECDsa_Console` project contains a single file named `Program.cs`, which provides an easy-to-understand implementation of the `LibServices` middleware and the `LibECDsa` backend.

> [!IMPORTANT]
> This project is a supplement to the [step-by-step ECDSA tutorial](https://www.youtube.com/watch?v=lPhWY9Euysc&list=PLp9W_V_LID_9ucXbhk0FrMxHjFUBt4uU6&index=2) and aims to provide an introduction to cryptography. 

> [!CAUTION]
> Use this code for educational purposes and avoid using it in production environments without proper security guarantees.

> [!WARNING]
> Some security steps to implement based on this code:
> - Avoid exposing the private key through public properties.
> - Do not persistently store `_privateKey`.
