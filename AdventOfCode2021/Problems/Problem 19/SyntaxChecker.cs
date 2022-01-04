using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Problems.Problem19
{
    public class SyntaxChecker
    {
        private List<Chunk> _subsystemLines;

        public SyntaxChecker()
        {
        }

        public void RegisterSubsystemLines(List<string> subsystemLines)
        {
            List<Chunk> listOfLines = new();
            foreach(var line in subsystemLines)
            {
                Chunk subsystemLine = new(line.First());
                for(int i = 1; i < line.Length; i++)
                {
                    if(!subsystemLine.TryToRegisterCharacter(line[i]))
                    {
                        break;
                    }
                }
                listOfLines.Add(subsystemLine);
            }

            _subsystemLines = listOfLines;
        }

        public List<char> GetInvalidClosingCharacters()
        {
            if(_subsystemLines.Count > 0)
            {
                List<char> invalidCharacters = new();
                foreach(var line in _subsystemLines)
                {
                    if(!line.IsValid)
                    {
                        invalidCharacters.Add(line.ClosingCharacter);
                    }
                }
                return invalidCharacters;
            }
            else
            {
                return new List<char>();
            }
        }

        private class Chunk
        {
            private Dictionary<char, char> OpeningAndClosingCharacters = new()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            public Chunk(char startingCharacter)
            {
                StartCharacter = startingCharacter;
                ClosingCharacter = '\0';
                IsValid = true;
            }

            public Chunk InnerChunk { get; set; }

            public Chunk TailingChunk { get; set; }

            private char StartCharacter { get; set; }

            public char ClosingCharacter { get; set; }

            public bool IsClosed {
                get
                {
                    return ClosingCharacter != '\0';
                }
            }

            public bool IsValid { get; private set; }

            public char FindLastChar()
            {
                if(IsClosed && TailingChunk != null)
                {
                    return TailingChunk.FindLastChar();
                }
                else if (!IsClosed && InnerChunk != null)
                {
                    return InnerChunk.FindLastChar();
                }
                else
                {
                    return ClosingCharacter;
                }
            }

            private List<char> GetExpectedClosingCharacter()
            {
                return OpeningAndClosingCharacters[StartCharacter].ToString().ToList();
            }

            public List<char> FindClosingCharacters()
            {
                if(!InnerChunk.IsClosed)
                {
                    var completeEndingCharacters = InnerChunk.FindClosingCharacters();
                    completeEndingCharacters.AddRange(GetExpectedClosingCharacter());
                    return completeEndingCharacters;
                }
                else if(!IsClosed)
                {
                    return GetExpectedClosingCharacter();
                }
                else
                {
                    return new List<char>();
                }
            }

            public bool TryToRegisterCharacter(char characterToRegister)
            {
                // check if inner chunk has been opened and not closed
                if(InnerChunk != null && !InnerChunk.IsClosed)
                {
                    // delegate to inner chunk
                    if(!InnerChunk.TryToRegisterCharacter(characterToRegister))
                    {
                        ClosingCharacter = characterToRegister;
                        IsValid = false;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    // check if closing character
                    if (OpeningAndClosingCharacters.ContainsValue(characterToRegister))
                    {
                        // check if closing character corresponds to opening character of this chunk
                        if (StartCharacter == OpeningAndClosingCharacters.Single(c => c.Value == characterToRegister).Key)
                        {
                            ClosingCharacter = characterToRegister;
                            IsValid = true;
                            return true;
                        }
                        else
                        {
                            ClosingCharacter = characterToRegister;
                            return false;
                        }
                    }
                    // check if opening character
                    else if (OpeningAndClosingCharacters.ContainsKey(characterToRegister))
                    {
                        if (IsClosed)
                        {
                            // create new tailing chunk if this chunk is closed
                            TailingChunk = new Chunk(characterToRegister);
                            return true;
                        }
                        else
                        {
                            // create inner chunk if this chunk is still opened
                            InnerChunk = new Chunk(characterToRegister);
                            return true;
                        }
                    }
                    // other character
                    else
                    {
                        ClosingCharacter = characterToRegister;
                        return false;
                    }
                }
            }
        }
    }
}
