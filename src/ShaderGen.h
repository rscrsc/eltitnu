#define MAX_INFOLOG_LEN 512

class ShaderGen
{
private:
    unsigned int outputProgram;
public:
    ShaderGen(const char* vertexPath, const char* fragmentPath);
    ~ShaderGen();
    // to use program generated
    void use();
    // to check if shader is compiled
    void checkCompileErrors(unsigned int shader);
    // to check if program is linked properly
    void checkLinkErrors(unsigned int program);
};
