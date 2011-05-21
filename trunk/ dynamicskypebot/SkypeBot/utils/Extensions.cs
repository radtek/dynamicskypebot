using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkypeBot.utils {
    public static class Extensions {
        public static String FormatWith(this String s, params object[] args) {
            return String.Format(s, args);
        }

        public static String FormatWith(this String s, object arg) {
            return String.Format(s, arg);
        }
    }
}
