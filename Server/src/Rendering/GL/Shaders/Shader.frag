#version 330

out vec4 outColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
  vec4 texel = texture(texture0, texCoord);
  if(texel.a == .0f)
    discard;
  outColor = texel;
}