#!/usr/bin/env python
import argparse
import sys
import os
from e52138 import E52138DiffUART, E52138AutoAddressingMaster

import time
COM_PORT = "COM50"

GUARD_IC_VERSION = "V0.9.2 21.06.2021"

def main(command_line=None):
    parser = argparse.ArgumentParser('Guard IC App')

    parser.add_argument('--port', nargs='?', help='Specify a COM Port for the DiffUARTBox, e.g. "COM30"')
    parser.add_argument('-v','--version', action='version', version=GUARD_IC_VERSION)

    subprasers = parser.add_subparsers(dest='command')
    show_errors = subprasers.add_parser('Show_Error_Codes',
                                     help='Displays the available error codes and the corresponding description')
    read_status = subprasers.add_parser('Read_Status_Flags',
                                     help='Reads out the current guard status flags')
    show_status = subprasers.add_parser('Show_Status_Flags',
                                     help='Displays the flag values which make up the answer of Read_Status_Flags')
    read_tst = subprasers.add_parser('Read_TST_Condition_Flag',
                                     help='Reads out the tst_cond flag')
    read_vbb_vbbp = subprasers.add_parser('Read_VBB_VBBP_Switch_Flag',
                                          help='Read the current status of the VBB_VBBP Switch')
    read_jtag_ol = subprasers.add_parser("Read_JTAG_REL_STRG_OL_FLAG",
                                         help='Returns the current OpenLoad flag of the Jtag Enable relais')
    read_jtag_sc = subprasers.add_parser("Read_JTAG_REL_STRG_SC_FLAG",
                                         help='Returns the current ShortCircuit flag of the Jtag Enable relais')
    read_fwver = subprasers.add_parser("FW_Version",
                                         help='Returns the read out firmware version datecode')
    read_rev = subprasers.add_parser("HW_Revision",
                                         help='Returns the read out revision if it is valid'
                                              ' or a negative value if revision is invalid')
    read_patch = subprasers.add_parser("HW_Patch",
                                         help='Returns the read out patch if it is valid '
                                              'or a negative value if patch is invalid')
    deactivate_tst = subprasers.add_parser("Deactivate_TST_Condition",
                                           help="Sends a deactivate TST_Condition command and returns answer of guard ic, "
                                                "0 on success, error_code on failure")
    activate_tst = subprasers.add_parser("Activate_TST_Condition",
                                           help="Sends an activate TST_Condition command and returns answer of guard ic, "
                                                "0 on success, error_code on failure")
    activate_vbb_vbbp = subprasers.add_parser("Activate_Switch_VBB_VBB_P",
                                           help="Sends an activate VBB-VBB_P switch to the Guard IC and "
                                                "returns the error code received by the guard,"
                                                " 0 on success, error_code on failure")
    deactivate_vbb_vbbp = subprasers.add_parser("Deactivate_Switch_VBB_VBB_P",
                                           help="Sends a deactivate VBB-VBB_P switch to the Guard IC and "
                                                "returns the error code received by the guard,"
                                                " 0 on success, error_code on failure")

    args = parser.parse_args(command_line)
    port = args.port
    save_stdout = sys.stdout
    sys.stdout = open(os.devnull, "w")
    with E52138DiffUART(com=port, diag=False, blocking=False, device_address=0xFF) as dut:
        # Check GUARD M-C
        sys.stdout = save_stdout
        if dut.get_fw_variant()[:9] != "GUARD M-C":
            return 'Variant Error'
        if args.command == 'Read_TST_Condition_Flag':
            status_flags = dut.GuardStatusFlags(dut.get_guard_flags())
            return status_flags.tst_condition_active_flag
        elif args.command == "Read_VBB_VBBP_Switch_Flag":
            status_flags = dut.GuardStatusFlags(dut.get_guard_flags())
            return status_flags.switch_vbb_vbbp_active_flag
        elif args.command == "Read_JTAG_REL_STRG_OL_FLAG":
            status_flags = dut.GuardStatusFlags(dut.get_guard_flags())
            return status_flags.jtag_rel_strg_ol_flag
        elif args.command == "Read_JTAG_REL_STRG_SC_FLAG":
            status_flags = dut.GuardStatusFlags(dut.get_guard_flags())
            return status_flags.jtag_rel_strg_sc_flag
        elif args.command == "Deactivate_TST_Condition":
            return dut.Deactivate_TST_Condition()
        elif args.command == "Activate_TST_Condition":
           return dut.Activate_TST_Condition()
        elif args.command == "Deactivate_Switch_VBB_VBB_P":
            return dut.Deactivate_Switch_VBB_VBB_P()
        elif args.command == "Activate_Switch_VBB_VBB_P":
            return dut.Activate_Switch_VBB_VBB_P()
        elif args.command == "HW_Revision":
            return dut.get_HW_Revision()
        elif args.command == "HW_Patch":
            return dut.get_HW_Patch()
        elif args.command == "FW_Version":
            return dut.get_fw_version()
        elif args.command == "Read_Status_Flags":
            return dut.get_guard_flags()
        elif args.command == "Show_Status_Flags":
            return dut.show_guard_flags()
        elif args.command == "Show_Error_Codes":
            return dut.show_error_codes()


if __name__ == '__main__':
    print(main())