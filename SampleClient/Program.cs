using Krenek.SSHKeysGenerator.Generators;

string privateKey;
string publicKey;
string publicPuTTYKey;

//RSA
using (var key = new RSAGenerator(4096))
{
    privateKey = key.ToPrivateKey();
    publicKey = key.ToPublicKey();
    publicPuTTYKey = key.ToPuTTYPublicKey();
}

Console.WriteLine(privateKey);
Console.WriteLine(publicKey);
Console.WriteLine(publicPuTTYKey);

//ECDsa
using (var key = new ECDsaGenerator("nistP521"))
{
    privateKey = key.ToPrivateKey();
    publicKey = key.ToPublicKey();
    publicPuTTYKey = key.ToPuTTYPublicKey();
}

Console.WriteLine(privateKey);
Console.WriteLine(publicKey);
Console.WriteLine(publicPuTTYKey);