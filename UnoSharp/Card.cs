using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp
{
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        public Card(CardValue value, CardColor color)
        {
            Value = value;
            Color = color;
            Type = ToType(value);
            DrawColor = ToColor(color);
        }
        private static readonly Color Blue = System.Drawing.Color.FromArgb(0x19, 0x76, 0xD2);
        private static readonly Color Yellow = System.Drawing.Color.FromArgb(0xFF, 0xEB, 0x3B);
        private static readonly Color Green = System.Drawing.Color.FromArgb(0x4C, 0xAF, 0x50);
        private static readonly Color Red = System.Drawing.Color.FromArgb(0xF4, 0x43, 0x36);
        private static readonly Color Wild = System.Drawing.Color.FromArgb(163, 154, 141);

        public CardColor Color { get; }
        public Color DrawColor { get; }
        public CardValue Value { get; }
        public CardType Type { get; }
        public int ValueNumber => (int) Value;
        public static Card[] CardsPool { get; } = GenerateDefaultCards();
        public static Image MainCardImage { get; } = GetMainCard();

        public static Card Generate()
        {
            return CardsPool.PickOne();
        }

        public static IEnumerable<Card> Generate(int num)
        {
            for (var i = 0; i < num; i++)
            {
                yield return Generate();
            }
        }

        private static Card[] GenerateDefaultCards()
        {
            var list = new List<Card>();
            for (var i = 0; i < 10 + 5; i++)
            {
                var value = (CardValue) i;
                var chance = GetChance(value);
                for (var j = 0; j < chance; j++)
                {
                    foreach (var color in GetDefaultColors(value))
                    {
                        list.Add(new Card(value, color));
                    }
                }
            }

            return list.ToArray();
        }

        public static int GetChance(CardValue value)
        {
            switch (value)
            {
                case CardValue.Zero:
                    return 1;
                default: // non zero number
                    return 2;
            }
        }

        public static IEnumerable<CardColor> GetDefaultColors(CardValue value)
        {
            switch (value)
            {
                case CardValue.DrawFour:
                case CardValue.Wild:
                    yield return CardColor.Wild;
                    yield return CardColor.Wild;
                    break;
                default:
                    yield return CardColor.Red;
                    yield return CardColor.Blue;
                    yield return CardColor.Yellow;
                    yield return CardColor.Green;
                    break;
            }
        }

        public static CardType ToType(CardValue value)
        {
            switch (value)
            {
                case CardValue.Reverse: return CardType.Reverse;
                case CardValue.Skip: return CardType.Skip;
                case CardValue.DrawTwo: return CardType.DrawTwo;
                case CardValue.DrawFour: return CardType.DrawFour;
                case CardValue.Wild: return CardType.Wild;
                default: return CardType.Number;
            }
        }
        
        
        public static Color ToColor(CardColor color)
        {
            switch (color)
            {
                case CardColor.Red:
                    return Red;
                case CardColor.Yellow:
                    return Yellow;
                case CardColor.Green:
                    return Green;
                case CardColor.Blue:
                    return Blue;
                case CardColor.Wild:
                    return Wild;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }

        public int CompareTo(Card other)
        {
            return ((int) this).CompareTo(((int)other));
        }

        public override string ToString()
        {
            switch (Type)
            {
                case CardType.Number:
                    return $"{Color} {(int) Value}";
                default:
                    return $"{Color} {Value}";
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Card);
        }

        public bool Equals(Card other)
        {
            return other != null &&
                   Color == other.Color &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -1984162526; // gen by Reshaper
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Card card1, Card card2)
        {
            return EqualityComparer<Card>.Default.Equals(card1, card2);
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

        public static explicit operator int(Card card)
        {
            return 15 * (int) card.Color + card.ValueNumber;
        }

        public static Image GetMainCard()
        {
            var path = $"UnoSharp.Resources.Cards.MainCard.png";
            var content = EmbedResourceReader.GetStream(path);
            return new Bitmap(content);
        }
    }

    public static class CardExtensions
    {
        public static string ToShortString(this Card card)
        {
            var sb = new StringBuilder();
            switch (card.Color)
            {
                case CardColor.Wild:
                    break;
                case CardColor.Red:
                    sb.Append("R");
                    break;
                case CardColor.Yellow:
                    sb.Append("Y");
                    break;
                case CardColor.Green:
                    sb.Append("G");
                    break;
                case CardColor.Blue:
                    sb.Append("B");
                    break;
            }
            switch (card.Type)
            {
                case CardType.Wild:
                    sb.Append("W");
                    break;
                case CardType.DrawTwo:
                    sb.Append("+2");
                    break;
                case CardType.Number:
                    sb.Append(card.ValueNumber);
                    break;
                case CardType.Reverse:
                    sb.Append("R");
                    break;
                case CardType.Skip:
                    sb.Append("S");
                    break;
                case CardType.DrawFour:
                    sb.Append("+4");
                    break;
            }

            return sb.ToString();
        }

        public static bool ContainsType(this IEnumerable<Card> cards, CardType type)
        {
            return cards.Any(card => card.Type == type);
        }

        public static bool ContainsColor(this IEnumerable<Card> cards, CardColor color)
        {
            return cards.Any(card => card.Color == color);
        }

        public static bool ContainsValue(this IEnumerable<Card> cards, CardValue value)
        {
            return cards.Any(card => card.Value == value);
        }

        public static bool IsValidForPlayerAndRemove(this Card card, Player player)
        {
            for (var index = 0; index < player.Cards.Count; index++)
            {
                var playerCard = player.Cards[index];
                switch (card.Type)
                {
                    case CardType.Number:
                    case CardType.Skip:
                    case CardType.Reverse:
                    case CardType.DrawTwo:
                        if (playerCard.Value == card.Value && playerCard.Color == card.Color)
                        {
                            player.Cards.Remove(playerCard);
                            return true;
                        }
                        continue;
                    case CardType.DrawFour:
                    case CardType.Wild:
                        if (playerCard.Value == card.Value)
                        {
                            player.Cards.Remove(playerCard);
                            return true;
                        }
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return false;
        }
        
    }
    public enum CardColor
    {
        Red,
        Yellow,
        Green,
        Blue,

        Wild
    }

    public enum CardValue
    {
        Zero,

        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,

        Reverse,
        Skip,
        DrawTwo,

        Wild,
        DrawFour
    }

    public enum CardType
    {
        Number,

        Reverse,
        Skip,
        DrawTwo,

        Wild,
        DrawFour
    }
}
