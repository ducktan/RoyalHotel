

#include <iostream>

#include <stdio.h>
using std::cerr;
using std::cin;
using std::cout;
using std::endl;

#include <stdexcept>
using std::runtime_error;

#include <chrono>

#include <string>
using std::string;

#include <exception>
using std::exception;

#include <sstream>

// Crypto++ Libraries
#include "cryptopp/base64.h"
using CryptoPP::Base64Decoder;
using CryptoPP::Base64Encoder;

#include "cryptopp/hex.h"
using CryptoPP::HexDecoder;
using CryptoPP::HexEncoder;

#include "cryptopp/cryptlib.h"
using CryptoPP::AuthenticatedSymmetricCipher;
using CryptoPP::BufferedTransformation;
using CryptoPP::DecodingResult;
using CryptoPP::Exception;
using CryptoPP::PrivateKey;
using CryptoPP::PublicKey;

#include "cryptopp/files.h"
using CryptoPP::FileSink;
using CryptoPP::FileSource;

#include "cryptopp/filters.h"
using CryptoPP::ArraySink;
using CryptoPP::AuthenticatedDecryptionFilter;
using CryptoPP::AuthenticatedEncryptionFilter;
using CryptoPP::HashVerificationFilter;
using CryptoPP::PK_DecryptorFilter;
using CryptoPP::PK_EncryptorFilter;
using CryptoPP::Redirector;
using CryptoPP::StreamTransformationFilter;
using CryptoPP::StringSink;
using CryptoPP::StringSource;

#include "cryptopp/osrng.h"
using CryptoPP::AutoSeededRandomPool;

#include "cryptopp/queue.h"
using CryptoPP::ByteQueue;

#include "cryptopp/secblock.h"
using CryptoPP::SecByteBlock;

// DES Libary
#include "cryptopp/aes.h"
using CryptoPP::AES;

// Confidentiality only modes
#include "cryptopp/modes.h"
using CryptoPP::CBC_Mode;
using CryptoPP::CFB_Mode;
using CryptoPP::CTR_Mode;
using CryptoPP::ECB_Mode;
using CryptoPP::OFB_Mode;

// Confidentiality and Authentication modes
#include "cryptopp/ccm.h"
using CryptoPP::CCM;

#include "cryptopp/eax.h"
using CryptoPP::EAX;

#include "cryptopp/gcm.h"
using CryptoPP::GCM;

#include "cryptopp/xts.h"
using CryptoPP::XTS;
#include "cryptopp/ccm.h"
using CryptoPP::CCM;
#include "cryptopp/gcm.h"
using CryptoPP::GCM;

// Define platform-specific features
#ifdef _WIN32
#include <Windows.h>
#endif

void SaveKey(const string &filename, const CryptoPP::SecByteBlock &bt)
{
    StringSource(bt, bt.size(), true,
                 new FileSink(filename.c_str(), true)); // StringSource
}

void SaveIV(const string &filename, const CryptoPP::SecByteBlock &bt)
{
    StringSource(bt, bt.size(), true,
                 new FileSink(filename.c_str(), true)); // StringSource
}

void SaveKeyBase64(const string &filename, const CryptoPP::SecByteBlock &bt)
{
    StringSource(bt, bt.size(), true,
                 new Base64Encoder(
                     new FileSink(filename.c_str(), false)) // Base64Encoder
    );                                                      // StringSource
}

void SaveIVBase64(const string &filename, const CryptoPP::SecByteBlock &bt)
{
    StringSource(bt, bt.size(), true,
                 new Base64Encoder(
                     new FileSink(filename.c_str(), false)) // Base64Encoder
    );                                                      // StringSource
}

void SaveKeyHex(const string &filename, const CryptoPP::SecByteBlock &bt)
{
    StringSource(bt, bt.size(), true,
                 new HexEncoder(
                     new FileSink(filename.c_str(), false)) // HexEncoder
    );                                                      // StringSource
    // HexEncoder encoder(new FileSink(filename.c_str(), false)); // HexEncoder
    // encoder.Put(bt, bt.size());
    // encoder.MessageEnd();
}

void SaveIVHex(const string &filename, const CryptoPP::SecByteBlock &bt)
{
    StringSource(bt, bt.size(), true,
                 new HexEncoder(
                     new FileSink(filename.c_str(), false)) // HexEncoder
    );
    // HexEncoder encoder(new FileSink(filename.c_str(), false)); // HexEncoder
    // encoder.Put(bt, bt.size());
    // encoder.MessageEnd();
}

// Load key & iv
void LoadKey(const string &filename, CryptoPP::SecByteBlock &bt)
{
    FileSource(filename.c_str(), true,
               new ArraySink(bt, bt.size()), true); // FileSource
}

void LoadIV(const string &filename, CryptoPP::SecByteBlock &bt)
{
    FileSource(filename.c_str(), true,
               new ArraySink(bt, bt.size()), true); // FileSource
}

void LoadKeyBase64(const string &filename, CryptoPP::SecByteBlock &bt)
{
    FileSource(filename.c_str(), true,
               new Base64Decoder(
                   new ArraySink(bt, bt.size())) /*Base64Decoder*/,
               false); // FileSource
}

void LoadIVBase64(const string &filename, CryptoPP::SecByteBlock &bt)
{
    FileSource(filename.c_str(), true,
               new Base64Decoder(
                   new ArraySink(bt, bt.size())) /*Base64Decoder*/,
               false);
}
void LoadKeyHex(const string &filename, CryptoPP::SecByteBlock &bt)
{
    FileSource(filename.c_str(), true,
               new HexDecoder(
                   new ArraySink(bt, bt.size())) /*Base64Decoder*/,
               false); // FileSource
}

void LoadIVHex(const string &filename, CryptoPP::SecByteBlock &bt)
{
    FileSource(filename.c_str(), true,
               new HexDecoder(
                   new ArraySink(bt, bt.size())) /*Base64Decoder*/,
               false);
}

string ConvertToPEM(const string &cipher)
{
    string pemCipher;
    StringSource(cipher, true,
                 new Base64Encoder(
                     new StringSink(pemCipher)));
    return pemCipher;
}

string ConvertToHex(const string &cipher)
{
    string hexCipher;
    StringSource(cipher, true,
                 new HexEncoder(
                     new StringSink(hexCipher)));
    return hexCipher;
}

string ConvertFromPEM(const string &pemCipher)
{
    string decodedCipher;
    StringSource(pemCipher, true,
                 new Base64Decoder(
                     new StringSink(decodedCipher)));
    return decodedCipher;
}

string ConvertFromHex(const string &hexCipher)
{
    string decodedCipher;
    StringSource(hexCipher, true,
                 new HexDecoder(
                     new StringSink(decodedCipher)));
    return decodedCipher;
}

std::streamsize getFileSize(const std::string &filename)
{
    // Mở tệp
    std::ifstream file(filename, std::ios::binary | std::ios::ate);
    if (!file.is_open())
    {
        std::cerr << "Error opening file: " << filename << std::endl;
        return -1; // Trả về -1 nếu có lỗi
    }

    // Lấy kích thước tệp
    std::streamsize size = file.tellg();
    file.close();

    return size;
}

string standardizeString(const char *mode)
{
    string result = mode;
    bool isLowerCase = true;

    // Kiểm tra xem tất cả các ký tự có phải là chữ cái viết thường không
    for (char c : result)
    {
        if (isalpha(c) && isupper(c))
        {
            isLowerCase = false;
            break;
        }
    }

    // Nếu tất cả các ký tự đều viết thường, chuyển đổi thành chữ in hoa
    if (isLowerCase)
    {
        transform(result.begin(), result.end(), result.begin(), [](unsigned char c)
                  { return std::toupper(c); });
    }

    return result;
}

size_t GetKeySizeFromFile(const std::string &filename, const std::string &format)
{
    size_t keySize = 0;

    std::ifstream file(filename, std::ios::binary);
    if (!file.is_open())
    {
        std::cerr << "Failed to open file: " << filename << std::endl;
        exit(1);
    }

    std::stringstream buffer;
    buffer << file.rdbuf();
    file.close();

    // Xác định độ dài của key dựa trên định dạng
    if (format == "PEM")
    {
        std::string key;
        StringSource(buffer.str(), true, new Base64Decoder(new StringSink(key)));
        keySize = key.size() * 8; // Đổi từ byte sang bit
    }
    else if (format == "HEX")
    {
        keySize = buffer.str().size() * 4; // Mỗi ký tự HEX biểu diễn 4 bit
    }
    else if (format == "DER")
    {
        keySize = buffer.str().size() * 8; // Đổi từ byte sang bit
    }
    else
    {
        cerr << "Unsupported format: " << format << std::endl;
        exit(1);
    }

    return keySize;
}

// Generate and save keys

// Generate and save keys
void GenerateAndSaveIV_Keys(const int KeySize, const char *KeyFormat, const char *KeyFileName)
{

    AutoSeededRandomPool prng;
    string strKeyFormat = standardizeString(KeyFormat);
    string strKeyFileName(KeyFileName);

    // Generate key & iv
    CryptoPP::SecByteBlock key(KeySize);
    prng.GenerateBlock(key, key.size());

    CryptoPP::SecByteBlock iv(AES::BLOCKSIZE);
    prng.GenerateBlock(iv, iv.size());

    // Save key & iv
    if (strKeyFormat == "DER")
    {
        SaveKey(strKeyFileName, key);
        SaveIV("iv.bin", iv);
    }
    else if (strKeyFormat == "PEM")
    {
        SaveKeyBase64(strKeyFileName, key);
        SaveIVBase64("iv.pem", iv);
    }
    else if (strKeyFormat == "HEX")
    {
        SaveKeyHex(strKeyFileName, key);
        SaveIVHex("iv.hex", iv);
    }
    else
    {
        cerr << "Unsupported key format. Please choose 'DER', 'PEM', or 'HEX'\n";
        exit(1);
    }
    cout << "----------------------------------------------------------" << endl;
    cout << "Successfully generate Key and IV! Key and IV saved to: " << strKeyFileName << " and iv file" << endl;
    cout << "----------------------------------------------------------" << endl;
}

void LoadKeyFromFile(const string &KeyFile, SecByteBlock &key, const string &strKeyFormat)
{
    if (strKeyFormat == "DER")
        LoadKey(KeyFile, key);
    else if (strKeyFormat == "PEM")
        LoadKeyBase64(KeyFile, key);
    else if (strKeyFormat == "HEX")
        LoadKeyHex(KeyFile, key);
    else
    {
        cerr << "Unsupported key format. Please choose 'DER', 'PEM' or 'HEX'!\n";
        exit(1);
    }
}

void LoadIVFromFile(SecByteBlock &iv, const string &strKeyFormat)
{
    if (strKeyFormat == "DER")
        LoadIV("iv.bin", iv);
    else if (strKeyFormat == "PEM")
        LoadIVBase64("iv.pem", iv);
    else if (strKeyFormat == "HEX")
        LoadIVHex("iv.hex", iv);
    else
    {
        cerr << "Unsupported key format. Please choose 'DER', 'PEM' or 'HEX'!\n";
        exit(1);
    }
}

void LoadKeyIVFromFile(const string &KeyFile, SecByteBlock &key, SecByteBlock &iv, const string &strMode, const string &strKeyFormat)
{
    // Load key
    // For XTS mode, key and IV are loaded separately
    LoadKeyFromFile(KeyFile, key, strKeyFormat);

    // Load IV
    if (strMode == "CBC" || strMode == "CFB" || strMode == "OFB" || strMode == "CTR" || strMode == "GCM" || strMode == "CCM" || strMode == "XTS")
        LoadIVFromFile(iv, strKeyFormat);
}

string LoadCipherText(const char *CipherFile, const string &strCipherFormat)
{
    // Load cipher
    string cipher;
    if (strCipherFormat == "DER" || strCipherFormat == "PEM" || strCipherFormat == "HEX")
    {
        // Load ciphertext from file with the specified format
        string encodedCipher;
        FileSource(CipherFile, true, new StringSink(encodedCipher));

        if (strCipherFormat == "PEM")
        {
            // Decode Base64 ciphertext
            StringSource(encodedCipher, true,
                         new Base64Decoder(new StringSink(cipher)));
        }
        else if (strCipherFormat == "HEX")
        {
            // Decode HEX ciphertext
            StringSource(encodedCipher, true,
                         new HexDecoder(new StringSink(cipher)));
        }
        else
        {
            // Use ciphertext as is (DER format)
            cipher = encodedCipher;
        }
    }
    else
    {
        cerr << "Unsupported cipher format. Please choose 'DER', 'PEM' or 'HEX'\n";
        exit(1);
    }
    return cipher;
}

string LoadPlainText(const char *PlaintextFile, const string &strPlaintextFormat)
{
    string plain;
    if (strPlaintextFormat == "Text")
    {
        FileSource(PlaintextFile, true, new StringSink(plain), false);
    }
    else if (strPlaintextFormat == "DER")
    {
        FileSource(PlaintextFile, true, new StringSink(plain), true);
    }
    else if (strPlaintextFormat == "PEM")
    {
        FileSource(PlaintextFile, true, new Base64Decoder(new StringSink(plain)), false);
    }
    else if (strPlaintextFormat == "HEX")
    {
        FileSource(PlaintextFile, true, new HexDecoder(new StringSink(plain)), false);
    }
    else
    {
        cerr << "Unsupported plain text format. Please choose 'Text', 'DER', 'PEM' or 'HEX'!\n";
        exit(1);
    }
    return plain;
}

void SaveCipherFile(const string &cipher, const string &strCipherFormat, const char *CipherFile)
{

    if (strCipherFormat == "DER")
    {
        StringSource(cipher, true,
                     new FileSink(CipherFile, true));
    }
    else if (strCipherFormat == "PEM")
    {
        StringSource(cipher, true,
                     new Base64Encoder(
                         new FileSink(CipherFile, false)));
        // cout << "PEM format cipher: " << ConvertToPEM(cipher) << endl;
    }
    else if (strCipherFormat == "HEX")
    {
        StringSource(cipher, true,
                     new HexEncoder(
                         new FileSink(CipherFile, false)));
        // cout << "HEX format cipher: " << ConvertToHex(cipher) << endl;
    }
    else
    {
        cerr << "Unsupported cipher format. Please choose 'DER', 'PEM' or 'HEX'\n";
        exit(1);
    }
}

void SaveRecoveredFile(const string &recovered, const string &strPlaintextFormat, const char *RecoveredFile)
{
    // Save recovered
    if (strPlaintextFormat == "Text")
    {
        StringSource(recovered, true,
                     new FileSink(RecoveredFile, false));
        // cout << "Recover: " << recovered << endl;
    }
    else if (strPlaintextFormat == "DER")
    {
        StringSource(recovered, true,
                     new FileSink(RecoveredFile, true));
    }
    else if (strPlaintextFormat == "PEM")
    {
        StringSource(recovered, true,
                     new Base64Encoder(
                         new FileSink(RecoveredFile, false)));
    }
    else if (strPlaintextFormat == "HEX")
    {
        StringSource(recovered, true,
                     new HexEncoder(
                         new FileSink(RecoveredFile, false)));
    }
    else
    {
        cerr << "Unsupported recovery text format. Please choose 'text', 'DER', 'PEM' or 'HEX'!\n";
        exit(1);
    }
}
// Encryption
void Encryption(const char *mode, const char *KeyFile, const char *KeyFormat, const char *PlaintextFile, const char *PlaintextFormat, const char *CipherFile, const char *CipherFormat)
{
    string strMode = standardizeString(mode);
    string strKeyFormat = standardizeString(KeyFormat);
    string strKeyFile(KeyFile);
    string strPlaintextFormat(PlaintextFormat);
    string strCipherFormat(CipherFormat);

    size_t KeySize = GetKeySizeFromFile(strKeyFile, strKeyFormat);
    KeySize /= 8;

    // cout << KeySize << endl;
    // Load key & iv
    CryptoPP::SecByteBlock key(KeySize);
    SecByteBlock iv(AES::BLOCKSIZE);

    LoadKeyIVFromFile(KeyFile, key, iv, strMode, strKeyFormat);

    string plain;

    if (PlaintextFile == nullptr)
    {
        cout << "Enter the plaintext: ";
        getline(cin, plain);
        cout << "Size of plain text: " << plain.size() << "bytes\n";
    }
    // Load plaintext
    else
    {
        plain = LoadPlainText(PlaintextFile, strPlaintextFormat);
    }

    string cipher;
    if (strMode == "ECB")
    {
        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            ECB_Mode<AES>::Encryption e;
            e.SetKey(key, key.size());

            StringSource(plain, true,
                         new StreamTransformationFilter(e,
                                                        new StringSink(cipher)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: ECB" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
    }

    else if (strMode == "CBC")
    {
        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng
            CBC_Mode<AES>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);

            StringSource(plain, true,
                         new StreamTransformationFilter(e,
                                                        new StringSink(cipher)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: CBC" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
    }
    else if (strMode == "CFB")
    {
        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            CFB_Mode<AES>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);

            StringSource(plain, true,
                         new StreamTransformationFilter(e,
                                                        new StringSink(cipher)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: CFB" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
    }
    else if (strMode == "OFB")
    {
        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            OFB_Mode<AES>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);

            StringSource(plain, true,
                         new StreamTransformationFilter(e,
                                                        new StringSink(cipher)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: OFB" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
    }
    else if (strMode == "CTR")
    {

        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            CTR_Mode<AES>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);

            StringSource(plain, true,
                         new StreamTransformationFilter(e,
                                                        new StringSink(cipher)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: OFB" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
    }
    else if (strMode == "GCM")
    {
        auto start = std::chrono::high_resolution_clock::now();

        const int TAG_SIZE = 12;
        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            GCM<AES>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);

            StringSource(plain, true,
                         new AuthenticatedEncryptionFilter(e,
                                                           new StringSink(cipher), false, TAG_SIZE) // AuthenticatedEncryptionFilter
            );
        } // StringSource

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: GCM" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
    }
    else if (strMode == "CCM")
    {
        auto start = std::chrono::high_resolution_clock::now();

        const int TAG_SIZE = 8;

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            CCM<AES, TAG_SIZE>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);
            e.SpecifyDataLengths(0, plain.size(), 0);

            StringSource(plain, true,
                         new AuthenticatedEncryptionFilter(e,
                                                           new StringSink(cipher)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: CCM" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else if (strMode == "XTS")
    {
        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            cipher.clear(); // Đặt lại giá trị của cipher thành chuỗi rỗng

            XTS<AES>::Encryption e;
            e.SetKeyWithIV(key, key.size(), iv);

            StringSource(plain, true,
                         new StreamTransformationFilter(e,
                                                        new StringSink(cipher)));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: XTS" << endl;
        cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else
    {
        cerr << "Unsupported mode. Please choose: 'ECB', 'CBC', 'CFB', 'OFB', 'CTR'!\n";
        exit(1);
    }

    // Save cipher
    SaveCipherFile(cipher, strCipherFormat, CipherFile); // cout << "Cipher text: " << cipher << endl;

    // cout << "----------------------------------------------------------" << endl;
    // cout << "Encryption successful. Cipher text saved to: " << CipherFile << endl;
    // cout << "----------------------------------------------------------" << endl;
}

// Decryption
void Decryption(const char *mode, const char *KeyFile, const char *KeyFormat, const char *RecoveredFile, const char *RecoveredFormat, const char *CipherFile, const char *CipherFormat)
{
    string strMode = standardizeString(mode);
    string strKeyFile(KeyFile);

    string strKeyFormat = standardizeString(KeyFormat);
    string strPlaintextFormat(RecoveredFormat);
    string strCipherFormat(CipherFormat);

    size_t KeySize = GetKeySizeFromFile(strKeyFile, strKeyFormat);

    // Load key & iv
    CryptoPP::SecByteBlock key(KeySize / 8);
    CryptoPP::SecByteBlock iv(AES::BLOCKSIZE);

    LoadKeyIVFromFile(KeyFile, key, iv, strMode, strKeyFormat);

    string cipher;

    if (CipherFile == nullptr)
    {
        cout << "Enter the ciphertext: ";
        getline(cin, cipher);

        if (strCipherFormat == "PEM")
        {
            cipher = ConvertFromPEM(cipher);
        }
        else if (strCipherFormat == "HEX")
        {
            cipher = ConvertFromHex(cipher);
        }
        cout << "Size of cipher text: " << cipher.size() << "bytes\n";
    }
    else
    {
        cipher = LoadCipherText(CipherFile, strCipherFormat);
    }

    // Decryption
    string recovered;
    if (strMode == "ECB")
    {
        auto start = std::chrono::high_resolution_clock::now();
        ECB_Mode<AES>::Decryption d;
        d.SetKey(key, key.size());
        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();
            StringSource(cipher, true,
                         new StreamTransformationFilter(d,
                                                        new StringSink(recovered)));
        }

        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Decryption Test" << endl;
        cout << "Mode: ECB" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else if (strMode == "CBC")
    {
        auto start = std::chrono::high_resolution_clock::now();
        CBC_Mode<AES>::Decryption d;
        d.SetKeyWithIV(key, key.size(), iv);

        for (int i = 0; i < 10000; i++)
        {

            recovered.clear();

            StringSource(cipher, true,
                         new StreamTransformationFilter(d,
                                                        new StringSink(recovered)));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Decryption Test" << endl;
        cout << "Mode: CBC" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else if (strMode == "CFB")
    {
        auto start = std::chrono::high_resolution_clock::now();
        CFB_Mode<AES>::Decryption d;
        d.SetKeyWithIV(key, key.size(), iv);
        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();

            StringSource(cipher, true,
                         new StreamTransformationFilter(d,
                                                        new StringSink(recovered)));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Decryption Test" << endl;
        cout << "Mode: CFB" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else if (strMode == "OFB")
    {
        auto start = std::chrono::high_resolution_clock::now();
        OFB_Mode<AES>::Decryption d;
        d.SetKeyWithIV(key, key.size(), iv);
        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();

            StringSource(cipher, true,
                         new StreamTransformationFilter(d,
                                                        new StringSink(recovered)));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Decryption Test" << endl;
        cout << "Mode: OFB" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else if (strMode == "CTR")
    {
        auto start = std::chrono::high_resolution_clock::now();
        CTR_Mode<AES>::Decryption d;
        d.SetKeyWithIV(key, key.size(), iv);
        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();

            StringSource(cipher, true,
                         new StreamTransformationFilter(d,
                                                        new StringSink(recovered)));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Decryption Test" << endl;
        cout << "Mode: CTR" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }
    else if (strMode == "GCM")
    {

        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();
            GCM<AES>::Decryption d;
            const int TAG_SIZE = 12;
            d.SetKeyWithIV(key, key.size(), iv);
            AuthenticatedDecryptionFilter df(d,
                                             new StringSink(recovered),
                                             AuthenticatedDecryptionFilter::DEFAULT_FLAGS, TAG_SIZE); // AuthenticatedDecryptionFilter

            StringSource(cipher, true,
                         new Redirector(df)); // StringSource
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Decryption Test" << endl;
        cout << "Mode: GCM" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;

        // if (true == df.GetLastResult())
        //{
        //     cout << "Verified data." << endl
        //         << endl;
        // }
        // else
        //{
        //     cout << "Failed to verify data." << endl
        //         << endl;
        // }
    }
    else if (strMode == "CCM")
    {

        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();
            const int TAG_SIZE = 8;
            CCM<AES, TAG_SIZE>::Decryption d;
            d.SetKeyWithIV(key, key.size(), iv);
            d.SpecifyDataLengths(0, cipher.size() - TAG_SIZE, 0);
            AuthenticatedDecryptionFilter df(d,
                                             new StringSink(recovered));
            StringSource(cipher, true,
                         new Redirector(df));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: CCM" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
        // if (true == df.GetLastResult())
        //{
        //     cout << "Verified data." << endl
        //         << endl;
        // }
        // else
        //{
        //     cout << "Failed to verify data." << endl
        //         << endl;
        // }
    }
    else if (strMode == "XTS")
    {
        auto start = std::chrono::high_resolution_clock::now();

        for (int i = 0; i < 10000; i++)
        {
            recovered.clear();

            XTS<AES>::Decryption d;
            d.SetKeyWithIV(key, key.size(), iv);

            StringSource(cipher, true,
                         new StreamTransformationFilter(d,
                                                        new StringSink(recovered)));
        }
        auto stop = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
        cout << "\n----------------------------------------------------------" << endl;
        cout << "Overview AES Encryption Test" << endl;
        cout << "Mode: XTS" << endl;
        cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
        cout << "----------------------------------------------------------" << endl;
    }

    else
    {
        cerr << "Unsupported mode. Please choose: 'ECB', 'CBC', 'CFB', 'OFB', 'CTR'!\n";
        exit(1);
    }

    SaveRecoveredFile(recovered, strPlaintextFormat, RecoveredFile);

    // cout << "----------------------------------------------------------" << endl;
    // cout << "Decryption successful! Plaintext saved to: " << RecoveredFile << endl;
    // cout << "----------------------------------------------------------" << endl;
}

// Hàm để lấy kích thước của một tệp
void ProcessInput(const string &action, int argc, char *argv[])
{
    string mode = argv[2];
    if (action == "genkey")
    {
        if (argc != 5)
        {
            cerr << "Usage: " << argv[0] << " genkey <KeySize> <KeyFileFormat> <KeyFile>" << endl;
            return;
        }
        int KeySize = std::stoi(argv[2]);
        GenerateAndSaveIV_Keys(KeySize / 8, argv[3], argv[4]);
    }
    else if (action == "encrypt" && (argc == 8 || argc == 9))
    {

        const char *plaintextFile = nullptr;
        const char *ciphertextFile = argv[argc - 2];
        std::streamsize plainSize = 0;
        if (argc == 9)
        {
            // plaintextFile = argv[5];
            // plainSize = getFileSize(plaintextFile);
            // auto start = std::chrono::high_resolution_clock::now();

            Encryption(argv[2], argv[3], argv[4], argv[5], argv[6], argv[7], argv[8]);

            // auto stop = std::chrono::high_resolution_clock::now();
            // auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);

            // cout << "\n----------------------------------------------------------" << endl;
            // cout << "Overview AES Encryption Test" << endl;
            // cout << "Mode: " << argv[2] << endl;
            // cout << "\nPlaintext file: " << plaintextFile << endl;
            // cout << "Size of plaintext file: " << plainSize << " bytes" << endl;
            // std::streamsize cipher = getFileSize(ciphertextFile);

            // cout << "\nCiphertext file: " << ciphertextFile << endl;
            // cout << "Size of ciphertext file: " << cipher << " bytes" << endl;

            // cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
            // cout << "----------------------------------------------------------" << endl;

            // cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
        }
        else
        {
            auto start = std::chrono::high_resolution_clock::now();
            Encryption(argv[2], argv[3], argv[4], plaintextFile, argv[5], argv[6], argv[7]);
            auto stop = std::chrono::high_resolution_clock::now();
            auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);
            cout << "\n----------------------------------------------------------" << endl;

            cout << "Overview AES Encryption Test" << endl;
            cout << "Mode: " << argv[2] << endl;
            cout << "Use plaintext input from screen!\n";
            std::streamsize cipher = getFileSize(ciphertextFile);

            cout << "\nCiphertext file: " << ciphertextFile << endl;
            cout << "Size of ciphertext file: " << cipher << " bytes" << endl;

            cout << "\nEncryption time: " << duration.count() << " milliseconds" << endl;
            cout << "----------------------------------------------------------" << endl;

            cout.flush();
        }
    }
    else if (action == "decrypt" && (argc == 8 || argc == 9))
    {
        const char *cipherFile = nullptr;

        std::streamsize cipherSize = 0;
        if (argc == 9)
        {
            // cipherFile = argv[7];
            // cipherSize = getFileSize(cipherFile);
            // auto start = std::chrono::high_resolution_clock::now();

            Decryption(argv[2], argv[3], argv[4], argv[5], argv[6], argv[7], argv[8]);

            // auto stop = std::chrono::high_resolution_clock::now();
            // auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(stop - start);

            // cout << "Overview AES Decryption Test" << endl;
            // cout << "Mode: " << argv[2] << endl;
            // cout << "\nCiphertext file: " << cipherFile << endl;
            // string rcvTextFile = argv[5];
            // std::streamsize rcv = getFileSize(rcvTextFile);
            // cout << "Size of cipher text file: " << cipherSize << " bytes" << endl;
            // cout << "\nRecovered text file: " << rcvTextFile << endl;
            // cout << "Size of recovered text file: " << rcv << " bytes" << endl;

            // cout << "\nDecryption time: " << duration.count() << " milliseconds" << endl;
            // cout << "----------------------------------------------------------" << endl;
            // cout.flush(); // Đảm bảo dữ liệu được ghi vào cout sẽ được xuất ra màn hình ngay lập tức
        }
        else
        {
            Decryption(argv[2], argv[3], argv[4], argv[5], argv[6], cipherFile, argv[7]);
            cout << "Overview AES Decryption Test" << endl;
            cout << "Mode: " << argv[2] << endl;
            cout << "Use cipher text input from screen!\n";
        }

        // string ciphertextFile = argv[7];
        // std::streamsize cipher = getFileSize(ciphertextFile);
        // cout << "----------------------------------------------------------" << endl;
    }
    else
    {
        cerr << "Please inputfollow to Usage!\n";
        exit(-1);
    }
}

int main(int argc, char *argv[])
{
    // Set UTF-8 Encoding
    // Set locale to support UTF-8
#ifdef _linux_
    std::locale::global(std::locale("C.utf8"));
#endif
#ifdef _WIN32
    // Set console code page to UTF-8 on Windows C.utf8, CP_UTF8
    SetConsoleOutputCP(CP_UTF8);
    SetConsoleCP(CP_UTF8);
#endif

    std::ios_base::sync_with_stdio(false);

    if (argc < 2)
    {
        cerr << "Invalid arguments. Please follow the instructions:\n";

        cerr << "Usage:\n"
             << "\t" << argv[0] << " genkey <KeySize> <KeyFileFormat> <KeyFile>\n"
             << "\t" << argv[0] << " encrypt <Mode> <KeyFile> <KeyFileFormat> [<PlaintextFile>] <PlaintextFormat> <CipherFile> <CipherFormat>\n"
             << "\t" << argv[0] << " decrypt <Mode> <KeyFile> <KeyFileFormat> <RecoveredFile> <RecoveredFileFormat> [<CipherFile>] <CipherFormat>\n"
             << "Note:\n"
             << "\tKeySize represents the size of the key in bits (128, 192, 256, 512(only XTS))\n"
             << "\tMode options: 'ECB', 'CBC', 'CFB', 'OFB', 'CTR', 'GCM', 'CCM', 'XTS (>=256)\n"
             << "\tOutputFormat options: 'DER', 'PEM', 'HEX'\n"
             << "\tKeyFormat & CiphertextFormat options: 'DER', 'PEM', 'HEX'\n"
             << "\tPlaintextFormat & RecoveredFormat options: 'Text', 'DER', 'PEM', 'HEX'\n\n"
             << "Example:\n"
             << "\t" << argv[0] << " genkey 256 DER key.bin\n"
             << "\t" << argv[0] << " encrypt CBC key.bin DER plain.txt Text cipher.bin DER\n"
             << "\t" << argv[0] << " decrypt CBC key.bin DER recovered.txt Text cipher.bin DER\n";

        return -1;
    }

    string action = argv[1];

    ProcessInput(action, argc, argv);

    return 0;
}