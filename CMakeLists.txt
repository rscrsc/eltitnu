cmake_minimum_required(VERSION 3.0.0)

set(CMAKE_TOOLCHAIN_FILE "$ENV{VCPKG_ROOT}/scripts/buildsystems/vcpkg.cmake")

project(main)
add_executable
(
     main
     "src/main.cpp"
     "src/ShaderGen.cpp"
     "src/ShaderGen.h"
)

# Libraries
find_package(glm CONFIG REQUIRED)
target_link_libraries(main PRIVATE glm::glm)

find_package(glfw3 CONFIG REQUIRED)
target_link_libraries(main PRIVATE glfw)

find_package(GLEW REQUIRED)
target_link_libraries(main PRIVATE GLEW::GLEW)