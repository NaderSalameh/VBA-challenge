Sub StockMarket()

    For Each ws In Worksheets


        '--.---------------.---------------.---------------
        ' INITIALIZING  VARIABLES
        ' --.---------------.---------------.---------------

        LastCol = ws.Cells(1, Columns.Count).End(xlToLeft).Column
        LastRow = ws.Cells(Rows.Count, 1).End(xlUp).Row
        YearOpen = ws.Cells(2, 3).Value
        YearClose = 0
        YearlyChange = 0
        VolAccum = 0
        PrevTicker = ws.Cells(2, 1).Value
        SummaryRow = 1

        'Creating the Summary Heading"
        ws.Range("I1").Value = "Ticker"
        ws.Range("J1").Value = "Yearly Change"
        ws.Range("K1").Value = "Percent Change"
        ws.Range("L1").Value = "Total Stock Volume"


        For Row = 2 To (LastRow + 1)

            If ws.Cells(Row, 1).Value = PrevTicker Then

             ' --.-Accumulating the total volume for each stock--.--
                VolAccum = VolAccum + ws.Cells(Row, 7).Value

            Else


            If ws.Cells(Row, 1).Value <> PrevTicker Then

                '  --.---------------.---------------.---------------.-----
                '   BEGIN CREATING THE SUMMARY FOR THE PREVIOUS STOCK
                '  --.---------------.---------------.---------------.-----

                ' -.--Updating the summary row--.-
                SummaryRow = SummaryRow + 1


                ' -.--Ticker Symbol--.-
                 ws.Cells(SummaryRow, 9).Value = PrevTicker


                '-.--Yearly Change--.-
                YearClose = ws.Cells(Row - 1, 6).Value
                YearlyChange = (YearClose - YearOpen)
                ws.Cells(SummaryRow, 10).Value = YearlyChange


                '-.-- Color Format for the Yearly Change--.-
                If YearlyChange > 0 Then
                    ws.Cells(SummaryRow, 10).Interior.ColorIndex = 4 ' green
                Else
                    ws.Cells(SummaryRow, 10).Interior.ColorIndex = 3 ' red
                End If


                ' -.--Percentage Change--.-
                If YearClose <> 0 And YearOpen <> 0 Then
                    ws.Cells(SummaryRow, 11).Value = FormatPercent((YearClose - YearOpen) / YearOpen, , vbTrue)
                Else
                    ws.Cells(SummaryRow, 11).Value = FormatPercent(0, , vbTrue)
                End If


                ' -.--Total Stock Volume--.-
                ws.Cells(SummaryRow, 12).Value = VolAccum

                '  --.---------------.---------------.---------------.-------
                '   DOING A RESET AND STARTING CALCS FOR THE CURRENT STOCK
                '  --.---------------.---------------.---------------.-------

                YearClose = 0
                VolAccum = 0
                YearOpen = ws.Cells(Row, 3).Value
                PrevTicker = ws.Cells(Row, 1).Value
                VolAccum = VolAccum + ws.Cells(Row, 7).Value

            End If
                    

        End If

    Next Row

    ' --.---------------.---------------.---------------.---------------.---------------
    '    CREATING THE SECOND SUMMARY
    ' --.---------------.---------------.---------------.---------------.---------------

    '  --.--Creating the labels for Second Summary--.--
    ws.Cells(2, 15).Value = "Greatest % Increase"
    ws.Cells(3, 15).Value = "Greatest % Decrease"
    ws.Cells(4, 15).Value = "Greatest Total Volume"
    ws.Cells(1, 16).Value = "Ticker"
    ws.Cells(1, 17).Value = "Value"


    ' -.-- greatest % increase with corresponding ticker
    ws.Cells(2, 17).Value = FormatPercent(WorksheetFunction.Max(ws.Range(ws.Cells(2, 11), ws.Cells(LastRow, 11))), , vbTrue)
    ws.Cells(2, 16).Value = WorksheetFunction.Index(ws.Range(ws.Cells(2, 9), ws.Cells(LastRow, 9)), WorksheetFunction.Match(ws.Cells(2, 17).Value, ws.Range(ws.Cells(2, 11), ws.Cells(LastRow, 11)), 0))


    '-.-- greatest % decrease with corresponding ticker
    ws.Cells(3, 17).Value = FormatPercent(WorksheetFunction.Min(ws.Range(ws.Cells(2, 11), ws.Cells(LastRow, 11))), , vbTrue)
    ws.Cells(3, 16).Value = WorksheetFunction.Index(ws.Range(ws.Cells(2, 9), ws.Cells(LastRow, 9)), WorksheetFunction.Match(ws.Cells(3, 17).Value, ws.Range(ws.Cells(2, 11), ws.Cells(LastRow, 11)), 0))


    ' Greatest total Volume with corresponding ticker
    ws.Cells(4, 17).Value = WorksheetFunction.Max(ws.Range(ws.Cells(2, 12), ws.Cells(LastRow, 12)))
    ws.Cells(4, 16).Value = WorksheetFunction.Index(ws.Range(ws.Cells(2, 9), ws.Cells(LastRow, 9)), WorksheetFunction.Match(ws.Cells(4, 17).Value, ws.Range(ws.Cells(2, 12), ws.Cells(LastRow, 12)), 0))



    Next ws

End Sub
