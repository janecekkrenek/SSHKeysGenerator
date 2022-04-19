#.Net Core SSH keys generator
Solution for creation SSH keys (RSA, ECDsa) using .NET.

## Supported Platforms
- .NET Standard 2.1
- .NET 5.0
- .NET 6.0

## Installation
SshKeysGenerator is a library for .NET and .NET core. It will be available at nuget.

## RSA Usage Example
```csharp
string privateKey;
string publicKey;
string publicPuTTYKey;

using (var key = new RSAGenerator(4096))
{
    privateKey = key.ToPrivateKey();
    publicKey = key.ToPublicKey();
    publicPuTTYKey = key.ToPuTTYPublicKey();
}

Console.WriteLine(privateKey);
Console.WriteLine(publicKey);
Console.WriteLine(publicPuTTYKey);
```

## ECDsa Usage Example
```csharp
string privateKey;
string publicKey;
string publicPuTTYKey;

using (var key = new ECDsaGenerator("nistP521"))
{
    privateKey = key.ToPrivateKey();
    publicKey = key.ToPublicKey();
    publicPuTTYKey = key.ToPuTTYPublicKey();
}

Console.WriteLine(privateKey);
Console.WriteLine(publicKey);
Console.WriteLine(publicPuTTYKey);
```

### Sample Output RSA

#### key.ToPrivateKey()
```text
-----BEGIN RSA PRIVATE KEY-----
MIIJKQIBAAKCAgEAnipRQEmo/So1VrB3cQyEYoU9QnCuqi8gnjBTK3t0re77J3Nb
6fGLcbX9oO4s3q7MbWaxY0XfxKLuTWaCmwh0pSpmp3HQslWlHEQBDqjZnAZ94ZRs
...
J+0DSB/+nxAa8Oox8fwejC7Gvc+PXDS5kAaimy90bZAu+MhmI5qotGD83C9RqDxi
uSH3g/C5c3X9XzxEcPTnme+cSw7l
-----END RSA PRIVATE KEY-----
```

#### key.ToPublicKey()
```text
-----BEGIN PUBLIC KEY-----
MIICCgKCAgEAnipRQEmo/So1VrB3cQyEYoU9QnCuqi8gnjBTK3t0re77J3Nb6fGL
...
AEjbYnejre6QsM06p99ta4QsGyXRJbAkd9Hw4+MG5DvPcfPTJT+UCAwEAAQ==
-----END PUBLIC KEY-----
```

#### key.ToPuTTYPublicKey()
```text
ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQCeKlFASaj9KjVWsHd
...
1rhCwbJdElsCR30fDj4wbkO89x89MlP5Q== imported-key
```

### Sample Output ECDsa nistP521

#### key.ToPrivateKey()
```text
-----BEGIN EC PRIVATE KEY-----
MIHcAgEBBEIBNNzbNUmNLmbUDj407lBmK07orAwFZqS+bSuv1evY8iKMKDPRJU0T
0gKxhfqn7a9PyhHphJI+F9ddFhFOW1CkYvCgBwYFK4EEACOhgYkDgYYABABJDCKZ
...
p100fw+jRWh9t9XPopL365nRY1OSP7HtHlaXttSdV6EjpLIdsW26cjnXA==
-----END EC PRIVATE KEY-----
```

#### key.ToPublicKey()
```text
-----BEGIN PUBLIC KEY-----
MIGbMBAGByqGSM49AgEGBSuBBAAjA4GGAAQASQwima9QKZBvuewi7DgvvWnr94a
...
6KS9+uZ0WNTkj+x7R5Wl7bUnVehI6SyHbFtunI51w=
-----END PUBLIC KEY-----
```

#### key.ToPuTTYPublicKey()
```text
ecdsa-sha2-nistp521 AAAAE2VjZHNhLXNoYTItbmlzdHA1MjEAAAAIbmlzdHA1MjEAAACFBABJDCKZr1Ap
...
h9t9XPopL365nRY1OSP7HtHlaXttSdV6EjpLIdsW26cjnXA== imported-key
```