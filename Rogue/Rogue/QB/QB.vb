Option Explicit On
Option Strict On
Option Infer On

Namespace Global

  Public NotInheritable Class QB

    Private Sub New()
    End Sub

    Public Shared Function QBChr(value%) As String
      Return QBChar(value).ToString
    End Function

    Public Shared Function QBChar(value%) As Char

      If value > -1 AndAlso value < 256 Then

        Select Case value
          Case 0 : Return " "c
          Case 1 : Return "☺"c
          Case 2 : Return "☻"c
          Case 3 : Return "♥"c
          Case 4 : Return "♦"c
          Case 5 : Return "♣"c
          Case 6 : Return "♠"c
          Case 7 : Return "●"c
          Case 8 : Return "◘"c
          Case 9 : Return "○"c
          Case 10 : Return "◙"c
          Case 11 : Return "♂"c
          Case 12 : Return "♀"c
          Case 13 : Return "♪"c
          Case 14 : Return "♫"c
          Case 15 : Return "✶"c
          Case 16 : Return "►"c
          Case 17 : Return "◄"c
          Case 18 : Return "↕"c
          Case 19 : Return "‼"c
          Case 20 : Return "¶"c
          Case 21 : Return "§"c
          Case 22 : Return "▬"c
          Case 23 : Return "↨"c
          Case 24 : Return "↑"c
          Case 25 : Return "↓"c
          Case 26 : Return "→"c
          Case 27 : Return "←"c
          Case 28 : Return "՟"c
          Case 29 : Return "↔"c
          Case 30 : Return "▲"c
          Case 31 : Return "▼"c
          Case 32 : Return " "c
          Case 32 To 125
            Return ChrW(value)
          Case 126 : Return "~"c
          Case 127 : Return "Δ"c
          Case 128 : Return "Ç"c
          Case 129 : Return "ü"c
          Case 130 : Return "é"c
          Case 131 : Return "â"c
          Case 132 : Return "ä"c
          Case 133 : Return "à"c
          Case 134 : Return "å"c
          Case 135 : Return "ç"c
          Case 136 : Return "ê"c
          Case 137 : Return "ë"c
          Case 138 : Return "è"c
          Case 139 : Return "ï"c
          Case 140 : Return "î"c
          Case 141 : Return "ì"c
          Case 142 : Return "Ä"c
          Case 143 : Return "Å"c
          Case 144 : Return "É"c
          Case 145 : Return "æ"c
          Case 146 : Return "Æ"c
          Case 147 : Return "ô"c
          Case 148 : Return "ö"c
          Case 149 : Return "ò"c
          Case 150 : Return "û"c
          Case 151 : Return "ù"c
          Case 152 : Return "ÿ"c
          Case 153 : Return "Ö"c
          Case 154 : Return "Ü"c
          Case 155 : Return "¢"c
          Case 156 : Return "£"c
          Case 157 : Return "¥"c
          Case 158 : Return "₧"c
          Case 159 : Return "ƒ"c
          Case 160 : Return "á"c
          Case 161 : Return "í"c
          Case 162 : Return "ó"c
          Case 163 : Return "ú"c
          Case 164 : Return "ñ"c
          Case 165 : Return "Ñ"c
          Case 166 : Return "ª"c
          Case 167 : Return "º"c
          Case 168 : Return "¿"c
          Case 169 : Return "⌐"c
          Case 170 : Return "¬"c
          Case 171 : Return "½"c
          Case 172 : Return "¼"c
          Case 173 : Return "¡"c
          Case 174 : Return "«"c
          Case 175 : Return "»"c
          Case 176 : Return "░"c
          Case 177 : Return "▒"c
          Case 178 : Return "▓"c
          Case 179 : Return "│"c
          Case 180 : Return "┤"c
          Case 181 : Return "╡"c
          Case 182 : Return "╢"c
          Case 183 : Return "╖"c
          Case 184 : Return "╕"c
          Case 185 : Return "╣"c
          Case 186 : Return "║"c
          Case 187 : Return "╗"c
          Case 188 : Return "╝"c
          Case 189 : Return "╜"c
          Case 190 : Return "╛"c
          Case 191 : Return "┐"c
          Case 192 : Return "└"c
          Case 193 : Return "┴"c
          Case 194 : Return "┬"c
          Case 195 : Return "├"c
          Case 196 : Return "─"c
          Case 197 : Return "┼"c
          Case 198 : Return "╞"c
          Case 199 : Return "╟"c
          Case 200 : Return "╚"c
          Case 201 : Return "╔"c
          Case 202 : Return "╩"c
          Case 203 : Return "╦"c
          Case 204 : Return "╠"c
          Case 205 : Return "═"c
          Case 206 : Return "╬"c
          Case 207 : Return "╧"c
          Case 208 : Return "╨"c
          Case 209 : Return "╤"c
          Case 210 : Return "╥"c
          Case 211 : Return "╙"c
          Case 212 : Return "╘"c
          Case 213 : Return "╒"c
          Case 214 : Return "╓"c
          Case 215 : Return "╫"c
          Case 216 : Return "╪"c
          Case 217 : Return "┘"c
          Case 218 : Return "┌"c
          Case 219 : Return " "c ' ????
          Case 220 : Return "▄"c
          Case 221 : Return "▌"c
          Case 222 : Return "▐"c
          Case 223 : Return "▀"c
          Case 224 : Return "α"c
          Case 225 : Return "ß"c
          Case 226 : Return "Γ"c
          Case 227 : Return "π"c
          Case 228 : Return "Σ"c
          Case 229 : Return "σ"c
          Case 230 : Return "µ"c
          Case 231 : Return "τ"c
          Case 232 : Return "Φ"c
          Case 233 : Return "Θ"c
          Case 234 : Return "Ω"c
          Case 235 : Return "δ"c
          Case 236 : Return "∞"c
          Case 237 : Return "φ"c
          Case 238 : Return "ε"c
          Case 239 : Return "∩"c
          Case 240 : Return "≡"c
          Case 241 : Return "±"c
          Case 242 : Return "≥"c
          Case 243 : Return "≤"c
          Case 244 : Return "⌠"c
          Case 245 : Return "⌡"c
          Case 246 : Return "÷"c
          Case 247 : Return "≈"c
          Case 248 : Return "°"c
          Case 249 : Return "∙"c
          Case 250 : Return "·"c
          Case 251 : Return "√"c
          Case 252 : Return "ⁿ"c
          Case 253 : Return "²"c
          Case 254 : Return "█"c
          Case 255 : Return " "c

          Case 256 : Return "■"c

          Case Else
            Return " "c
        End Select

      Else
        Throw New ArgumentOutOfRangeException(NameOf(value))
      End If

    End Function

  End Class

End Namespace