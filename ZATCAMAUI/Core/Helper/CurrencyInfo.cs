namespace ZATCAMAUI.Core.Helper
{

    public class CurrencyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyInfo"/> class.
        /// </summary>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public CurrencyInfo(Currencies currency)
        {
            switch (currency)
            {
                case Currencies.GenericNumber:

                    CurrencyId = 0;
                    CurrencyCode = string.Empty;
                    IsCurrencyNameFeminine = true;
                    EnglishCurrencyName = string.Empty;
                    EnglishPluralCurrencyName = string.Empty;
                    EnglishCurrencyPartName = string.Empty;
                    EnglishPluralCurrencyPartName = string.Empty;
                    Arabic1CurrencyName = string.Empty;
                    Arabic2CurrencyName = string.Empty;
                    Arabic310CurrencyName = string.Empty;
                    Arabic1199CurrencyName = string.Empty;
                    Arabic1CurrencyPartName = string.Empty;
                    Arabic2CurrencyPartName = string.Empty;
                    Arabic310CurrencyPartName = string.Empty;
                    Arabic1199CurrencyPartName = string.Empty;
                    PartPrecision = 1;
                    IsCurrencyPartNameFeminine = true;
                    break;

                case Currencies.Syria:
                    CurrencyId = 0;
                    CurrencyCode = "SYP";
                    IsCurrencyNameFeminine = true;
                    EnglishCurrencyName = "Syrian Pound";
                    EnglishPluralCurrencyName = "Syrian Pounds";
                    EnglishCurrencyPartName = "Piaster";
                    EnglishPluralCurrencyPartName = "Piasteres";
                    Arabic1CurrencyName = "ليرة سورية";
                    Arabic2CurrencyName = "ليرتان سوريتان";
                    Arabic310CurrencyName = "ليرات سورية";
                    Arabic1199CurrencyName = "ليرة سورية";
                    Arabic1CurrencyPartName = "قرش";
                    Arabic2CurrencyPartName = "قرشان";
                    Arabic310CurrencyPartName = "قروش";
                    Arabic1199CurrencyPartName = "قرشاً";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = false;
                    break;

                case Currencies.Uae:
                    CurrencyId = 1;
                    CurrencyCode = "AED";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "UAE Dirham";
                    EnglishPluralCurrencyName = "UAE Dirhams";
                    EnglishCurrencyPartName = "Fils";
                    EnglishPluralCurrencyPartName = "Fils";
                    Arabic1CurrencyName = "درهم إماراتي";
                    Arabic2CurrencyName = "درهمان إماراتيان";
                    Arabic310CurrencyName = "دراهم إماراتية";
                    Arabic1199CurrencyName = "درهماً إماراتياً";
                    Arabic1CurrencyPartName = "فلس";
                    Arabic2CurrencyPartName = "فلسان";
                    Arabic310CurrencyPartName = "فلوس";
                    Arabic1199CurrencyPartName = "فلساً";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = false;
                    break;

                case Currencies.SaudiArabia:
                    CurrencyId = 2;
                    CurrencyCode = "SAR";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "Saudi Riyal";
                    EnglishPluralCurrencyName = "Saudi Riyals";
                    EnglishCurrencyPartName = "Halala";
                    EnglishPluralCurrencyPartName = "Halalas";
                    Arabic1CurrencyName = "ريال سعودي";
                    Arabic2CurrencyName = "ريالان سعوديان";
                    Arabic310CurrencyName = "ريالات سعودية";
                    Arabic1199CurrencyName = "ريالاً سعودياً";
                    Arabic1CurrencyPartName = "هللة";
                    Arabic2CurrencyPartName = "هللتان";
                    Arabic310CurrencyPartName = "هللات";
                    Arabic1199CurrencyPartName = "هللة";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = true;
                    break;

                case Currencies.Tunisia:
                    CurrencyId = 3;
                    CurrencyCode = "TND";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "Tunisian Dinar";
                    EnglishPluralCurrencyName = "Tunisian Dinars";
                    EnglishCurrencyPartName = "milim";
                    EnglishPluralCurrencyPartName = "millimes";
                    Arabic1CurrencyName = "دينار تونسي";
                    Arabic2CurrencyName = "ديناران تونسيان";
                    Arabic310CurrencyName = "دنانير تونسية";
                    Arabic1199CurrencyName = "ديناراً تونسياً";
                    Arabic1CurrencyPartName = "مليم";
                    Arabic2CurrencyPartName = "مليمان";
                    Arabic310CurrencyPartName = "ملاليم";
                    Arabic1199CurrencyPartName = "مليماً";
                    PartPrecision = 3;
                    IsCurrencyPartNameFeminine = false;
                    break;

                case Currencies.Gold:
                    CurrencyId = 4;
                    CurrencyCode = "XAU";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "Gram";
                    EnglishPluralCurrencyName = "Grams";
                    EnglishCurrencyPartName = "Milligram";
                    EnglishPluralCurrencyPartName = "Milligrams";
                    Arabic1CurrencyName = "جرام";
                    Arabic2CurrencyName = "جرامان";
                    Arabic310CurrencyName = "جرامات";
                    Arabic1199CurrencyName = "جراماً";
                    Arabic1CurrencyPartName = "ملجرام";
                    Arabic2CurrencyPartName = "ملجرامان";
                    Arabic310CurrencyPartName = "ملجرامات";
                    Arabic1199CurrencyPartName = "ملجراماً";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = false;
                    break;

                default: throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
            }
        }

        /// <summary>
        ///     The currencies.
        /// </summary>
        public enum Currencies
        {
            /// <summary>
            ///     The syria.
            /// </summary>
            Syria = 0,

            /// <summary>
            ///     The uae.
            /// </summary>
            Uae,

            /// <summary>
            ///     The saudi arabia.
            /// </summary>
            SaudiArabia,

            /// <summary>
            ///     The tunisia.
            /// </summary>
            Tunisia,

            /// <summary>
            ///     The gold.
            /// </summary>
            Gold,

            /// <summary>
            /// The generic number.
            /// </summary>
            GenericNumber
        }

        /// <summary>
        ///     Arabic Currency Name for 11 to 99 units
        ///     خمس و سبعون ليرةً سوريةً
        ///     خمسة و سبعون درهماً إماراتياً
        /// </summary>
        public string Arabic1199CurrencyName { get; set; }

        /// <summary>
        ///     Arabic Currency Part Name for 11 to 99 units
        ///     قرشاً
        ///     هللةً
        /// </summary>
        public string Arabic1199CurrencyPartName { get; set; }

        /// <summary>
        ///     Arabic Currency Name for 1 unit only
        ///     ليرة سورية
        ///     درهم إماراتي
        /// </summary>
        public string Arabic1CurrencyName { get; set; }

        /// <summary>
        ///     Arabic Currency Part Name for 1 unit only
        ///     قرش
        ///     هللة
        /// </summary>
        public string Arabic1CurrencyPartName { get; set; }

        /// <summary>
        ///     Arabic Currency Name for 2 units only
        ///     ليرتان سوريتان
        ///     درهمان إماراتيان
        /// </summary>
        public string Arabic2CurrencyName { get; set; }

        /// <summary>
        ///     Arabic Currency Part Name for 2 unit only
        ///     قرشان
        ///     هللتان
        /// </summary>
        public string Arabic2CurrencyPartName { get; set; }

        /// <summary>
        ///     Arabic Currency Name for 3 to 10 units
        ///     خمس ليرات سورية
        ///     خمسة دراهم إماراتية
        /// </summary>
        public string Arabic310CurrencyName { get; set; }

        /// <summary>
        ///     Arabic Currency Part Name for 3 to 10 units
        ///     قروش
        ///     هللات
        /// </summary>
        public string Arabic310CurrencyPartName { get; set; }

        /// <summary>
        ///     Standard Code
        ///     Syrian Pound: SYP
        ///     UAE Dirham: AED
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        ///     Currency ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     English Currency Name for single use
        ///     Syrian Pound
        ///     UAE Dirham
        /// </summary>
        public string EnglishCurrencyName { get; set; }

        /// <summary>
        ///     English Currency Part Name for single use
        ///     Piaster
        ///     Fils
        /// </summary>
        public string EnglishCurrencyPartName { get; set; }

        /// <summary>
        ///     English Plural Currency Name for Numbers over 1
        ///     Syrian Pounds
        ///     UAE Dirhams
        /// </summary>
        public string EnglishPluralCurrencyName { get; set; }

        /// <summary>
        ///     English Currency Part Name for Plural
        ///     Piasters
        ///     Fils
        /// </summary>
        public string EnglishPluralCurrencyPartName { get; set; }

        /// <summary>
        ///     Is the currency name feminine ( Mua'anath مؤنث)
        ///     ليرة سورية : مؤنث = true
        ///     درهم : مذكر = false
        /// </summary>
        public bool IsCurrencyNameFeminine { get; set; }

        /// <summary>
        ///     Is the currency part name feminine ( Mua'anath مؤنث)
        ///     هللة : مؤنث = true
        ///     قرش : مذكر = false
        /// </summary>
        public bool IsCurrencyPartNameFeminine { get; set; }

        /// <summary>
        ///     Decimal Part Precision
        ///     for Syrian Pounds: 2 ( 1 SP = 100 parts)
        ///     for Tunisian Dinars: 3 ( 1 TND = 1000 parts)
        /// </summary>
        public byte PartPrecision { get; set; }
    }
}