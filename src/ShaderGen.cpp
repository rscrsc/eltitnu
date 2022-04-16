#include "ShaderGen.h"

// std
#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <exception>

// OpenGL
#include <GL/glew.h>
#include <GLFW/glfw3.h>

ShaderGen::ShaderGen(const char *vertexPath, const char *fragmentPath)
{
    // read and convert shader source file to C-style string
    std::ifstream vertexFile;
    std::ifstream fragmentFile;
    std::stringstream vertexSStream;
    std::stringstream fragmentSStream;
    std::string vertexString;
    std::string fragmentString;
    const char* vertexSource;
    const char* fragmentSource;
    vertexFile.exceptions(std::ifstream::failbit | std::ifstream::badbit);
    fragmentFile.exceptions(std::ifstream::failbit | std::ifstream::badbit);
    try
    {
        vertexFile.open(vertexPath);
        fragmentFile.open(fragmentPath);

        vertexSStream << vertexFile.rdbuf();
        fragmentSStream << fragmentFile.rdbuf();

        vertexFile.close();
        fragmentFile.close();

        vertexString = vertexSStream.str();
        fragmentString = fragmentSStream.str();

        vertexSource = vertexString.c_str();
        fragmentSource = fragmentString.c_str();
    }
    catch (const std::exception &e)
    {
        std::cout << "cannot open shader file error: " << e.what() << std::endl;
    }

        // === compile shader source ===

        // - compile vertex shader
        unsigned int vertexShader = glCreateShader(GL_VERTEX_SHADER);
        glShaderSource(vertexShader, 1, &vertexSource, NULL);
        glCompileShader(vertexShader);
        checkCompileErrors(vertexShader);

        // - compile fragment shader
        unsigned int fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fragmentShader, 1, &fragmentSource, NULL);
        glCompileShader(fragmentShader);
        checkCompileErrors(fragmentShader);

        // - link shaders to generate outputProgram
        outputProgram = glCreateProgram();
        glAttachShader(outputProgram, vertexShader);
        glAttachShader(outputProgram, fragmentShader);
        glLinkProgram(outputProgram);
        checkLinkErrors(outputProgram);
}

void ShaderGen::use()
{
    glUseProgram(outputProgram);
}

void ShaderGen::checkCompileErrors(unsigned int shader)
{
    int success;
    char infoLog[MAX_INFOLOG_LEN];
    // check shader compiling
    glGetShaderiv(shader, GL_COMPILE_STATUS, &success);
    if(!success){
        glGetShaderInfoLog(shader, MAX_INFOLOG_LEN, NULL, infoLog);
        std::cout << "shader compile error: " << infoLog << std::endl;
    }
}

void ShaderGen::checkLinkErrors(unsigned int program)
{
    int success;
    char infoLog[MAX_INFOLOG_LEN];
    // check program linking
    glGetProgramiv(program, GL_LINK_STATUS, &success);
    if(!success){
        glGetProgramInfoLog(program, MAX_INFOLOG_LEN, NULL, infoLog);
        std::cout << "program link error: " << infoLog << std::endl;
    }
}

ShaderGen::~ShaderGen()
{
}